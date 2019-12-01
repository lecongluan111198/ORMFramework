using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DemoORM.Entity;

namespace DemoORM.Util
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

        public List<TableORM> getTables()
        {
            string queryString = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            List<TableORM> tables = new List<TableORM>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TableORM t = new TableORM();
                        t.TableName = reader[0].ToString();
                        t.collumn = GetColumns(t.TableName);
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

        public List<ColumnORM> GetColumns(string tableName)
        {
            string queryString =
                "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName";
            List<ColumnORM> columns = new List<ColumnORM>();
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
                        ColumnORM c = new ColumnORM();
                        c.columnName = reader[0].ToString();
                        c.dbType = reader[1].ToString();
                        c.type = parseDataType(reader[1].ToString());
                        columns.Add(c);

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

        public List<PrimaryORM> getPrimayKey(string table)
        {

            return null;
        }
        private string parseDataType(string type)
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
                case "nchar":
                case "numeric":
                case "smallmoney":
                    return "decimal";

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
