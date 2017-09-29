using System;
using System.Data;

namespace RDTools.Common
{

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


    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnMapAttribute : Attribute
    {
        private string columnName;
        private DbType dbtype;
        private string tableName;
        private bool nullable;
        private string aliasName;
        private int? maxLenght;
        private object defaultValue = null;

        public ColumnMapAttribute(string columnName, string tableName, string aliasName)
        {
            this.columnName = columnName;
            this.tableName = tableName;
            this.aliasName = aliasName;
        }

        public ColumnMapAttribute(string columnName, string tableName, DbType dbtype, bool nullable, string aliasName, int maxLenght)
        {
            this.columnName = columnName;
            this.tableName = tableName;
            this.dbtype = dbtype;
            this.nullable = nullable;
            this.aliasName = aliasName;
            this.maxLenght = maxLenght;
        }

        public ColumnMapAttribute(string columnName, string talbeName, DbType dbtype, bool nullable, string aliasName, int maxLenght, object defaultValue)
        {
            this.columnName = columnName;
            this.tableName = talbeName;
            this.dbtype = dbtype;
            this.nullable = nullable;
            this.aliasName = aliasName;
            this.maxLenght = maxLenght;
            this.defaultValue = defaultValue;
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public DbType DbType
        {
            get { return dbtype; }
            set { dbtype = value; }
        }

        public bool Nullable
        {
            get { return nullable; }
            set { nullable = value; }
        }

        public string Alias
        {
            get { return aliasName; }
            set { aliasName = value; }
        }

        public int? MaxLength
        {
            get { return maxLenght; }
            set { maxLenght = value; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class WindowDescribeAttribute : Attribute
    {
        public WindowDescribeAttribute(string _describe, string _className)
        {
            this.Describe = _describe;
            this.ClassName = _className;
        }

        public string Describe
        {
            get;
            set;
        }
        public string ClassName
        {
            get;
            set;
        }

    }
}
