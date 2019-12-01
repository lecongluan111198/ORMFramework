using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoORM.Entity;

namespace DemoORM.Util
{
    class SQLUtil
    {
        private static SQLUtil instance = null;
        public enum ACTION
        {
            SELECT_ALL,
            INSERT,
            UPDATE,
            DELETE,

        }
        public static SQLUtil INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new SQLUtil();
                return instance;
            }
        }

        public string createSQLText<T>(string tableName, ACTION action, T data)
        {
            switch (action)
            {
                case ACTION.SELECT_ALL:
                case ACTION.INSERT:
                case ACTION.UPDATE:
                case ACTION.DELETE:
                    return "DELETE FROM " + tableName + " WHERE @field = @id";
            }
            return null;
        }

        private string buildUpdateQuery<T>(T data) 
            where T : DetailEntity
        {
            string s = "UPDATE table_name SET column1 = value1, column2 = value2, ... WHERE @field = @id; ";
            StringBuilder sb = new StringBuilder(s);
            sb.AppendFormat("{0} SET ", data.tableName);
            for(int i = 0; i < data.amountOfColumn; i++)
            {
                if(i != data.amountOfColumn - 1)
                {
                    sb.AppendFormat("{0} = ", );
                }
            }
            return null;
        }
    }
}
