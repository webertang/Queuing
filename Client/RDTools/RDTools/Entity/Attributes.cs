using System;
using System.Data;

namespace RDTools.Entity
{
    /// <summary>
    /// 实体特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableMapAttribute : Attribute
    {
        private string tableName;
        private string[] primaryKeys;
        public TableMapAttribute(string tableName, params string[] primaryKeys)
        {
            this.tableName = tableName;
            this.primaryKeys = primaryKeys;
        }
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string[] PrimaryKeys
        {
            get { return primaryKeys; }
            set { primaryKeys = value; }
        }
    }

    /// <summary>
    /// 实体属性特性
    /// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ColumnMapAttribute : Attribute
	{
		private string columnName;
		private DbType dbtype;
        private string tableName;
        private bool nullable;
        private string alias;
        private int? maxLenght;
		private object defaultValue = null;

        public ColumnMapAttribute(string columnName, string tableName, string alias)
        {
            this.columnName = columnName;
            this.tableName = tableName;
            this.alias = alias;
        }

        public ColumnMapAttribute(string columnName, string tableName, DbType dbtype, bool nullable, string alias, int maxLenght)
		{
			this.columnName = columnName;
            this.tableName = tableName;
			this.dbtype = dbtype;
            this.nullable = nullable;
            this.alias = alias;
            this.maxLenght = maxLenght;
		}

		public ColumnMapAttribute(string columnName, string talbeName, DbType dbtype, bool nullable, string alias, int maxLenght, object defaultValue)
		{
			this.columnName = columnName;
            this.tableName = talbeName;
			this.dbtype = dbtype;
            this.nullable = nullable;
            this.alias = alias;
            this.maxLenght = maxLenght;
			this.defaultValue = defaultValue;
		}

		public string ColumnName
		{
			get{return columnName;}
			set{columnName = value;}
		}

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

		public DbType DbType
		{
			get{return dbtype;}
			set{dbtype = value;}
		}

        public bool Nullable
        {
            get { return nullable; }
            set { nullable = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public int? MaxLength
        {
            get { return maxLenght; }
            set { maxLenght = value; }
        }
		
		public object DefaultValue
		{
			get{return defaultValue;}
			set{defaultValue = value;}
		}
	}

    /// <summary>
    /// 枚举字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumAttribute : Attribute
    {
        public string Memo { get; set; }

        public EnumAttribute(string memo)
        {
            Memo = memo;
        }
    }
}
