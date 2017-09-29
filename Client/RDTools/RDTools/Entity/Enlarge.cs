using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RDTools.Entity
{
    /// <summary>
    /// 扩展方法类，包含所有对象的扩展方法
    /// </summary>
    public static class Enlarge
    {
        /// <summary>
        /// 转换为EntityList集合
        /// </summary>
        public static EntityList<Entity> ToEntityList<Entity>(this IEnumerable<Entity> list) where Entity : EntityBase, new()
        {
            if (list == null)
            {
                return null;
            }

            //通过新实例完成转换
            EntityList<Entity> entityList = new EntityList<Entity>();
            entityList.AddRange(list);

            return entityList;
        }

        /// <summary>
        /// 返回改变的实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static EntityList<Entity> ToChangeList<Entity>(this EntityList<Entity> list) where Entity : EntityBase, new()
        {
            if (list == null)
                return null;

            //只返回改变的实体
            EntityList<Entity> entityList = list.Where(o => o.EditState != EntityState.Unchange).ToEntityList();
            entityList.DeleteList = list.DeleteList;
            return entityList;
        }

        /// <summary>
        /// 将状态修改为默认 Unchange
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="list"></param>
        public static void ClearStatus<Entity>(this EntityList<Entity> list) where Entity : EntityBase, new()
        {
            if (list == null)
                return ;
            list.ForEach(o => o.EditState = EntityState.Unchange);
            list.DeleteList.Clear();
        }

        /// <summary>
        /// 提交数据时 清空状态
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="list"></param>
        /// <param name="submitAction"></param>
        public static void ToSubmit<Entity>(this EntityList<Entity> list,Action<EntityList<Entity>> submitAction) where Entity : EntityBase, new()
        {
            EntityList<Entity> entityList = list.ToChangeList<Entity>();
            submitAction(entityList);
            list.ClearStatus<Entity>();
        }
        public static void ToSubmit<Entity>(this Entity entity, Action<Entity> submitAction) where Entity : EntityBase, new()
        {
            submitAction(entity);
            entity.EditState = EntityState.Unchange;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        public static string GetTableName<Entity>(this Entity entity) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetCustomAttributes(typeof(TableMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                TableMapAttribute table = objs[0] as TableMapAttribute;

                if (table != null)
                {
                    return table.TableName;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        public static string[] GetPrimaryKeys<Entity>(this Entity entity) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetCustomAttributes(typeof(TableMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                TableMapAttribute table = objs[0] as TableMapAttribute;

                if (table != null)
                {
                    return table.PrimaryKeys;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取列名
        /// </summary>
        public static string GetColumnName<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.ColumnName;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取列表名
        /// </summary>
        public static string GetColumnTableName<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.TableName;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取列数据库类型
        /// </summary>
        public static DbType? GetDbType<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.DbType;
                }
            }

            return null;
        }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public static bool? GetColumnNullable<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.Nullable;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取别名
        /// </summary>
        public static string GetColumnAlias<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.Alias;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取最大长度
        /// </summary>
        public static int? GetColumnMaxLength<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.MaxLength;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        public static object GetColumnDefaultValue<Entity>(this Entity entity, string propertyName) where Entity : EntityBase
        {
            object[] objs = entity.GetType().GetProperty(propertyName).GetCustomAttributes(typeof(ColumnMapAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                ColumnMapAttribute column = objs[0] as ColumnMapAttribute;

                if (column != null)
                {
                    return column.DefaultValue;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取枚举批注
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumMemo(this Enum e)
        {
            object[] objs = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(EnumAttribute), true);

            //获取特性值
            if (objs.Length > 0)
            {
                EnumAttribute en = objs[0] as EnumAttribute;

                if (en != null)
                {
                    return en.Memo;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取字符串实际长度
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetFactLength(this string text)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] bytes = n.GetBytes(text);
            int length = 0;

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                //63为汉字字符或标点符号，同时也是?的ascii也是63，所以也要排除?
                if (bytes[i].ToString() == "63" && text[i] != '?')
                {
                    length++;
                }
                length++;
            }

            return length;
        }

        public static List<Entity> AllEntityClone<Entity>(this List<Entity> list) where Entity : EntityBase
        {
            List<Entity> returnlist = new List<Entity>();

            for (int i = 0; i < list.Count; i++)
            {
                returnlist.Add(list[i].Clone() as Entity);
            }

            return returnlist;
        }

        public static EntityList<Entity> CloneToEntityList<Entity>(this List<Entity> list) where Entity : EntityBase, new()
        {
            EntityList<Entity> returnlist = new EntityList<Entity>();

            for (int i = 0; i < list.Count; i++)
            {
                returnlist.Add(list[i].Clone() as Entity);
            }

            return returnlist;
        }

        public static EntityList<Entity> ToEntityList<Entity>(this List<Entity> list) where Entity : EntityBase, new()
        {
            EntityList<Entity> returnlist = new EntityList<Entity>();

            for (int i = 0; i < list.Count; i++)
            {
                returnlist.Add(list[i] as Entity);
            }

            return returnlist;
        }
    }
}
