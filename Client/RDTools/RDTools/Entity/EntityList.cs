using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace RDTools.Entity
{
    /// <summary>
    /// 对象实体集合
    /// </summary>
    [SerializableAttribute]
    public class EntityList <Entity>: List<Entity> where Entity : EntityBase, new()
    {
        //定义包括备份集合、删除集合和删除备份集合
        protected List<Entity> _backupList = new List<Entity>();
        protected List<Entity> _deleteList = new List<Entity>();
        protected List<Entity> _deleleBackupList = new List<Entity>();

        public List<Entity> DeleteList
        {
            get { return _deleteList; }
            set { _deleteList = value; }
        }

        public object Tag { get; set; }

        //重新定义FindAll方法
        public new IEnumerable<Entity> FindAll(Predicate<Entity> match)
        {
            return base.FindAll(match);
        }

        //克隆方法，调用个实体的克隆方法
        public EntityList<Entity> Clone()
        {
            EntityList<Entity> list = new EntityList<Entity>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(this[i].Clone() as Entity);
            }

            return list;
        }

        //克隆方法，调用个实体的克隆方法
        public List<Entity> CloneToList()
        {
            List<Entity> list = new List<Entity>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(this[i].Clone() as Entity);
            }

            return list;
        }

        #region 以下为重新定义所有Remove方法，主要用于把删除的项保存在删除集合中

        public new bool Remove(Entity item)
        {  
            if (base.Remove(item))
            {
                if (item.EditState != EntityState.Add)
                {
                    _deleteList.Add(item);
                }
                return true;
            }

            return false;
        }

        public new void RemoveAt(int index)
        {
            Entity item = this[index];
            if (item.EditState != EntityState.Add)
            {
                _deleteList.Add(this[index]);
            }
            base.RemoveAt(index);
        }

        public new void RemoveAll(Predicate<Entity> match)
        {
            
            _deleteList.AddRange(FindAll(match).Where(o=>o.EditState != EntityState.Add));
            base.RemoveAll(match);
        }

        public new void RemoveRange(int index, int count)
        {
            if (index >= Count || (index + count) > Count)
            {
                return;
            }

            for (int i = index; i < count; i++)
            {
                RemoveAt(i);
            }

            base.RemoveRange(index, count);
        }

        #endregion

        /// <summary>
        /// 备份数据
        /// </summary>
        public void Backup()
        {
            _backupList = CloneToList();
            _deleleBackupList = _deleteList.AllEntityClone();
        }

        /// <summary>
        /// 同意修改，修改后备份
        /// </summary>
        public void AcceptChange()
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].AcceptChange();
            }

            _backupList = CloneToList();
            _deleteList.Clear();
            _deleleBackupList.Clear();
        }

        /// <summary>
        /// 判断数据是否改变
        /// </summary>
        /// <returns></returns>
        public bool HasChanged()
        {
            if(_deleteList.Count != 0)
            {
                return true;
            }

            if (_backupList == null)
            {
                return false;
            }

            //调用对比方法
            return !EqualList(_backupList.ToEntityList());
        }

        /// <summary>
        /// 取消改变，返回备份的集合
        /// </summary>
        /// <returns></returns>
        public EntityList<Entity> Chancel()
        {
            if (_backupList == null)
            {
                return this;
            }

            EntityList<Entity> list = _backupList.CloneToEntityList();
            list.DeleteList = _deleleBackupList.AllEntityClone();
            list.Backup();

            return list;
        }

        /// <summary>
        /// 比较两实体是否一样
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool EqualList(EntityList<Entity> list)
        {
            if (list == null || list.Count != Count)
            {
                return false;
            }

            //循环比较各个字段
            for (int i = 0; i < Count; i++)
            {
                bool isChange = true;
                PropertyInfo[] pros = this[i].GetType().GetProperties();

                for (int j = 0; j < list.Count; j++)
                {
                    bool allEqual = true;

                    foreach (PropertyInfo pro in pros)
                    {
                        if (pro.Name == "HaveNoUseForValidateColumnList" || pro.Name == "Error" || pro.Name == "Tag")
                        {
                            continue;
                        }
                        object a = pro.GetValue(this[i], null);
                        object b  =list[j].GetType().GetProperty(pro.Name).GetValue(list[j], null);

                        bool isEqual = false;
                        if (a != null && b != null)
                        {
                            if (a.ToString() != b.ToString())
                            {
                                isEqual = true;
                            }
                        }

                        if (a == null && a != b || isEqual) 
                        {
                            allEqual = false;
                            break;
                        }
                    }

                    if (allEqual)
                    {
                        isChange = false;
                        break;
                    }
                }

                if (isChange)
                {
                    return false;
                }
            }

            return true;
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
