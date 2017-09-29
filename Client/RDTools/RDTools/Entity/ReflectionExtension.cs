using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace RDTools.Entity
{
    /// <summary>
    /// 反射调用类
    /// </summary>
    public static  class ReflectionExtension
    {
        /// <summary>
        /// 不考虑大小写 获取属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public static PropertyInfo GetIgnPropertyInfo(this Type type, string name)
        {
            return type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        }

        /// <summary>
        /// 获得真实类型（对Nullable进行了筛选）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Type GetPropertyType(this PropertyInfo info)
        {
            Type pType = info.PropertyType;
            if (pType.IsGenericType && pType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return pType.GetGenericArguments()[0];
            }
            return pType;
        }




        /// <summary>
        /// 将实体a的属性赋值到实体b中，如果b中不存在a的属性则跳过
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void EntityConvert<T1, T2>(T1 a, T2 b)
            where T1 : class, new()
            where T2 : class, new()
        {
            Type t1 = a.GetType();
            foreach (PropertyInfo property in t1.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                object obj = t1.GetValue(a, property.Name);
                if (obj == null)
                    continue;
                SetValue(b, property.Name, obj);
            }
        }

        #region Expression
        #region SetValue
        /// <summary>
        /// 设置实体某属性的值(非静态属性)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        ///<typeparam name="V">属性的类型</typeparam>
        /// <param name="fieldName">属性名称</param>
        /// <param name="fieldValue">要设置的值</param>
        public static void SetValue<T,V>(this T entity, string fieldName, V fieldValue) where T:class
        {
            if (entity == null)
                return ;
            Type objType = typeof(T);
            string methodName = "set_" + fieldName;
            Action<object, object> objAction = null;
            if (HttpRuntimeCache.IsCacheExist(objType.Name + methodName))
            {
                objAction = HttpRuntimeCache.GetCache<Action<object, object>>(objType.Name + methodName);
            }
            else
            {
                MethodInfo method = objType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (method == null)
                    return;
                Type type = method.GetParameters()[0].ParameterType;
                var param_obj = Expression.Parameter(typeof(object), "obj");
                var param_value = Expression.Parameter(typeof(object), "value");

                var convert_obj = Expression.Convert(param_obj, method.ReflectedType);
                var convert_value = Expression.Convert(param_value, type);

                var callExpression = Expression.Call(convert_obj, method, convert_value);
                Expression<Action<object, object>> lambdaExpression = Expression.Lambda<Action<object, object>>(callExpression, param_obj, param_value);
                var Action = lambdaExpression.Compile();
                objAction = (object o1, object o2) =>
                {
                    Action.SetTypeValue(o1, type, o2);
                };
                HttpRuntimeCache.SetCache(objType.Name + fieldName, objAction, 10);
            }
            objAction(entity, fieldValue);
        }

        private static void SetTypeValue(this  Action<object, object> setAction, object entity, Type fieldType, object fieldValue)
        {
            try
            {

                //如果是枚举类型，考虑与计算的情况   a | b
                if (fieldType.IsEnum)
                {
                    int enValue = 0;
                    foreach (string filed in fieldValue.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        enValue = enValue | (int)Enum.Parse(fieldType, filed);
                    }
                    setAction(entity, enValue);
                    return;
                }

                //如果是值类型  进行强制转换
                if (fieldValue.ToString() == string.Empty)
                {
                    return;
                }
                if (fieldType.IsValueType)
                {
                    setAction(entity, fieldType.GetMethod("Parse", new Type[] { typeof(string) }).Invoke(null, new object[] { fieldValue.ToString() }));
                }
                //object类型 直接赋值
                else
                {
                    setAction(entity, fieldValue);
                }
            }
            catch
            {
                throw new Exception(string.Format("\"{0}\"  格式 \"{1}\" 转换失败，请检查。值为:{2}", fieldType.BaseType.Name, fieldType.Name, fieldValue));
            }
        }
        #endregion

        #region GetValue
        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="fieldName">属性名称（不区分大小写）</param>
        /// <returns>如果找不到属性则返回null 否则返回应有的值</returns>
        public static object GetValue<T>(this T entity, string fieldName) where T:class
        {
            if (entity == null)
                return null;
            Type objType = typeof(T);
            return objType.GetValue(entity, fieldName);
        }
        public static object GetValue(this Type objType, object entity, string fieldName)
        {
            Func<object, object> objAction = null;
            string methodName = "get_" + fieldName;
            if (HttpRuntimeCache.IsCacheExist(objType.Name + methodName))
            {
                objAction = HttpRuntimeCache.GetCache<Func<object, object>>(objType.Name + methodName);
            }
            else
            {
                MethodInfo method = objType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (method == null)
                    return null;

                var param_obj = Expression.Parameter(typeof(object), "obj");
                var convert_obj = Expression.Convert(param_obj, method.ReflectedType);
                var callExpression = Expression.Call(convert_obj, method);

                var castMethodCall = Expression.Convert(callExpression, typeof(object));
                Expression<Func<object, object>> lambdaExpression = Expression.Lambda<Func<object, object>>(castMethodCall, param_obj);
                objAction = lambdaExpression.Compile();

                HttpRuntimeCache.SetCache(objType.Name + fieldName, objAction, 10);

            }
            return objAction(entity);
        }
        #endregion

        #region CreateInstance
        public static T CreateInstance<T>(this Type type)
        {
            if (HttpRuntimeCache.IsCacheExist(type.FullName))
            {
                return (T)HttpRuntimeCache.GetCache<Func<object>>(type.FullName)();
            }
            else
            {
                NewExpression newExp = Expression.New(type);
                Expression<Func<object>> lambdaExp = Expression.Lambda<Func<object>>(newExp, null);
                Func<object> func = lambdaExp.Compile();
                HttpRuntimeCache.SetCache(type.FullName, func, 10);
                return (T)func();
            }
        }
        public static T CreateInstance<T>(this Type type, Type[] parameterTypes, object[] objs)
        {
            if (HttpRuntimeCache.IsCacheExist(type.FullName))
            {
                return (T)HttpRuntimeCache.GetCache<Func<object[],object>>(type.FullName)(objs);
            }
            else
            {
                //根据参数类型数组来获取构造函数
                var constructor = type.GetConstructor(parameterTypes);
                //创建lambda表达式的参数
                var lambdaParam = Expression.Parameter(typeof(object[]), "_args");
                //创建构造函数的参数表达式数组
                var constructorParam = new List<Expression>();
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    //从参数表达式（参数是：object[]）中取出参数
                    var arg = BinaryExpression.ArrayIndex(lambdaParam, Expression.Constant(i));
                    //把参数转化成指定类型
                    var argCast = Expression.Convert(arg, parameterTypes[i]);

                    constructorParam.Add(argCast);
                }
                //创建构造函数表达式
                NewExpression newExp = Expression.New(constructor, constructorParam);
                //创建lambda表达式，返回构造函数
                Expression<Func<object[], object>> lambdaExp =
                    Expression.Lambda<Func<object[], object>>(newExp, lambdaParam);

                Func<object[], object> func = lambdaExp.Compile();

                HttpRuntimeCache.SetCache(type.FullName, func, 10);
                return (T)func(objs);
            }
        }
        /// <param name="assemblyPath">***.dll</param>
        /// <param name="className">namespace + . +className</param>
        /// <param name="parameterTypes">构造函数参数类别</param>
        /// <param name="objs">构造函数参数</param>
        public static T CreateInstance<T>(string assemblyPath, string className, Type[] parameterTypes, object[] objs)
        {
            Assembly assembly = Assembly.LoadFile(assemblyPath);
            return assembly.GetType(className).CreateInstance<T>(parameterTypes,objs);
        }
        /// <param name="assemblyPath">***.dll</param>
        /// <param name="className">namespace + className</param>
        public static T CreateInstance<T>(string assemblyPath, string className)
        {
            Assembly assembly = Assembly.LoadFile(assemblyPath);
            return assembly.GetType(className).CreateInstance<T>();
        }
        #endregion
        #endregion
    }
}
