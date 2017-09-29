using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace RDTools.Entity
{
    /// <summary>
    /// 实体对象基类
    /// </summary>
    [SerializableAttribute]
    public class EntityBase
    {
        protected bool _isInitalize = false;
        [NonSerialized]
        protected string _error = string.Empty;
        protected EntityBase _backup = null;
        protected List<string> _haveNoUseForValidateColumnList = new List<string>();

        /// <summary>
        /// 实体状态
        /// </summary>
        public EntityState EditState { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error 
        {
            get { return _error;}
            protected set { _error = value; }
        }

        /// <summary>
        /// 不需要验证的列名集合
        /// </summary>
        public List<string> HaveNoUseForValidateColumnList
        {
            get { return _haveNoUseForValidateColumnList; }
            set { _haveNoUseForValidateColumnList = value; }
        }

        public object Tag { get; set; }

        public EntityBase()
        {
            EditState = EntityState.Add;
        }

        /// <summary>
        /// 确定修改
        /// </summary>
        public void AcceptChange()
        {
            EditState = EntityState.Unchange;
            _backup = this.Clone() as EntityBase;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        public void MakeToDelete()
        {
            EditState = EntityState.Delete;
        }

        /// <summary>
        /// 开始初始化，不改变状态
        /// </summary>
        public void BeginInitialize()
        {
            _isInitalize = true;
        }

        /// <summary>
        /// 结束初始化
        /// </summary>
        public void EndInitalize()
        {
            _isInitalize = false;
        }

        /// <summary>
        /// 克隆实体，逐个属性进行克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Type type = this.GetType();
            EntityBase entity = Activator.CreateInstance(type) as EntityBase;
            PropertyInfo[] pros = GetType().GetProperties();

            foreach (PropertyInfo pro in pros)
            {
                if (pro.Name != "HaveNoUseForValidateColumnList" && pro.Name != "EditState" && pro.Name != "Error" && pro.Name != "Tag")
                {
                    entity.GetType().GetProperty(pro.Name).SetValue(entity, pro.GetValue(this, null), null);
                }
            }

            entity.EditState = EntityState.Unchange;

            return entity;
        }

        /// <summary>
        /// 备份，克隆一份备份
        /// </summary>
        public void Backup()
        {
            _backup = Clone() as EntityBase;
        }

        /// <summary>
        /// 取消修改
        /// </summary>
        /// <returns></returns>
        public EntityBase Chancel()
        {
            if (_backup == null)
            {
                return this;
            }

            EntityBase entity = _backup.Clone() as EntityBase;

            return entity;
        }

        /// <summary>
        /// 比较两实体是否相同，逐个属性比较
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EqualEntity(EntityBase entity)
        {
            PropertyInfo[] pros = GetType().GetProperties();

            foreach (PropertyInfo pro in pros)
            {
                if (pro.Name == "HaveNoUseForValidateColumnList" || pro.Name == "EditState" || pro.Name == "Error" || pro.Name == "Tag")
                {
                    continue;
                }

                if ((entity.GetType().GetProperty(pro.Name).GetValue(entity, null) == null && pro.GetValue(this, null) != entity.GetType().GetProperty(pro.Name).GetValue(entity, null)) ||
                    (entity.GetType().GetProperty(pro.Name).GetValue(entity, null).ToString() != pro.GetValue(this, null).ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 非空及字符串超长验证
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            Error = string.Empty;

            System.Reflection.PropertyInfo[] ps = GetType().GetProperties();
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].Name == "HaveNoUseForValidateColumnList" || ps[i].Name == "EditState" || ps[i].Name == "Error" || ps[i].Name == "Tag")
                {
                    continue;
                }

                if (this.GetColumnTableName(ps[i].Name) == this.GetTableName())
                {
                    if (!HaveNoUseForValidateColumnList.Contains(this.GetColumnName(ps[i].Name)) && this.GetColumnNullable(ps[i].Name).HasValue
                        && !this.GetColumnNullable(ps[i].Name).Value)
                    {
                        if (ps[i].GetValue(this, null) == null || (ps[i].GetValue(this, null) != null && this.GetDbType(ps[i].Name) == DbType.String && ps[i].GetValue(this, null).ToString() == string.Empty))
                        {
                            Error += "“" + (string.IsNullOrEmpty(this.GetColumnAlias(ps[i].Name)) ? this.GetColumnName(ps[i].Name) : this.GetColumnAlias(ps[i].Name)) + "”不能为空！\r\n";
                        }                     
                    }

                    if (ps[i].GetValue(this, null) != null)
                    {
                        if (!HaveNoUseForValidateColumnList.Contains(ps[i].Name) && this.GetDbType(ps[i].Name).HasValue && this.GetDbType(ps[i].Name).Value == DbType.String &&
                            this.GetColumnMaxLength(ps[i].Name).HasValue && this.GetColumnMaxLength(ps[i].Name).Value - 1 < ps[i].GetValue(this, null).ToString().GetFactLength())
                        {
                            Error += "“" + (string.IsNullOrEmpty(this.GetColumnAlias(ps[i].Name)) ? this.GetColumnName(ps[i].Name) : this.GetColumnAlias(ps[i].Name)) + "”超出了指定长度！\r\n";
                        }
                    }
                }
            }

            Error = Error.TrimEnd('\n', '\r');

            return string.IsNullOrEmpty(Error);
        }

        /// <summary>
        /// 扩展
        /// </summary>
        /// <param name="func"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object Enlarge(Func<object, object> func, object obj)
        {
            return func(obj);
        }
    }
}
