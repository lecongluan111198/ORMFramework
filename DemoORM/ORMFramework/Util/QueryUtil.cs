using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ORMFramework.Entity;

namespace ORMFramework.Util
{
    class QueryUtil
    {
        private static QueryUtil instance = null;

        public static QueryUtil INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new QueryUtil();
                return instance;
            }
        }

        private string connectionString = 
            "Data Source=DESKTOP-G7ODJ9B\\SQLEXPRESS;Initial Catalog=ManagementSystem;Integrated Security=True";

        public void setConnectionString(string connect)
        {
            this.connectionString = connect;
        }

        public List<EntityInfo> getTables()
        {
            string queryString = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            List<EntityInfo> tables = new List<EntityInfo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        EntityInfo t = new EntityInfo();
                        t.name = reader[0].ToString();
                        t.columns = GetColumns(t.name);
                        t.bindingName = FormatName(t.name);
                        t.primaryKey = GetPrimaryKey(t.name);
                        t.foreignKeys = GetForeignKey(t.name);
                        tables.Add(t);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return tables;
        }

        public EntityInfo getTables(string tableName)
        {
            EntityInfo table = new EntityInfo();
            table.name = tableName;
            table.columns = GetColumns(tableName);
            table.bindingName = FormatName(tableName);
            table.primaryKey = GetPrimaryKey(tableName);
            table.foreignKeys = GetForeignKey(tableName);
            return table;
        }

        public Dictionary<string, ColumnInfo> GetColumns(string tableName)
        {
            string queryString =
                "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName";
            Dictionary<string, ColumnInfo> columns = new Dictionary<string, ColumnInfo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tableName", tableName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ColumnInfo c = new ColumnInfo();
                        c.name = reader[0].ToString();
                        c.DBType = reader[1].ToString();
                        c.bindingName = FormatName(c.name);
                        c.type = ParseDataType(reader[1].ToString());
                        columns.Add(c.name, c);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return columns;
        }

        public PrimaryKey GetPrimaryKey(string tableName)
        {
            string queryString =
                "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                "WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 " +
                "AND TABLE_NAME = @tableName";

            PrimaryKey pk = new PrimaryKey();
            List<string> columns = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tableName", tableName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        columns.Add(reader[0].ToString());
                    }
                    reader.Close();
                    pk.columns = columns;
                    pk.references = GetPrimaryReferences(tableName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return pk;
        }

        public List<ForeignKey> GetForeignKey(string tableName)
        {
            string queryString =
                "SELECT CU.COLUMN_NAME, PK.TABLE_NAME, PT.COLUMN_NAME  " +
                "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C " +
                    "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME " +
                    "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME " +
                    "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME " +
                    "INNER JOIN( " +
                        "SELECT i1.TABLE_NAME, i2.COLUMN_NAME " +
                        "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 " +
                            "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME " +
                        "WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT ON PT.TABLE_NAME = PK.TABLE_NAME " +
                        "WHERE FK.TABLE_NAME = @tableName";

            List<ForeignKey> foreigns = new List<ForeignKey>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tableName", tableName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ForeignKey fk = new ForeignKey();
                        fk.column = reader[0].ToString();
                        KeyValuePair<string, string> reference = new KeyValuePair<string, string>(reader[1].ToString(), reader[2].ToString());
                        fk.reference = reference;
                        foreigns.Add(fk);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foreigns;
        }

        private Dictionary<string, string> GetPrimaryReferences(string tableName)
        {
            string queryString =
                "SELECT FK.TABLE_NAME, PT.COLUMN_NAME " +
                "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C " +
                    "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME " +
                    "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME " +
                    "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME " +
                    "INNER JOIN( " +
                        "SELECT i1.TABLE_NAME, i2.COLUMN_NAME " +
                        "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 " +
                            "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME " +
                        "WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT " +
                        "ON PT.TABLE_NAME = PK.TABLE_NAME " +
                 "WHERE PK.TABLE_NAME = @tablename";

            Dictionary<string, string> references = new Dictionary<string, string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@tableName", tableName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        references.Add(reader[0].ToString(), reader[1].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return references;
        }


        private string FormatName(string name)
        {
            return name;
        }

        private string ParseDataType(string type)
        {
            switch (type)
            {
                case "bigint":
                case "timestamp":
                    return "long";

                case "binary":
                case "image":
                case "varbinary":
                    return "byte[]";

                case "smalldatetime":
                case "date":
                case "datetime":
                case "datetime2":
                    return "DateTime";

                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return "decimal";

                case "nchar":
                case "nvarchar":
                case "ntext":
                case "varchar":
                case "text":
                    return "string";
                case "real":
                    return "float";
                case "float":
                    return "double";
                case "bit":
                    return "bool";
                case "char":
                    return "char";
                case "int":
                    return "int";
                case "smallint":
                    return "short";
                case "time":
                    return "TimeSpan";
                case "tinyint":
                    return "byte";
                case "uniqueidentifier":
                    return "Guid";
                case "datetimeoffset":
                    return "DateTimeOffset";
            }
            return "";
        }

    }
}
