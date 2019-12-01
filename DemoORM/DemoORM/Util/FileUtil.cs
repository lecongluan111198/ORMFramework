using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DemoORM.Entity;

namespace DemoORM.Util
{
    class FileUtil
    {
        private static FileUtil instance = null;

        public static FileUtil INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new FileUtil();
                return instance;
            }
        }

        
        string tableTemplate = @"..\..\FileTemplate\table.txt";

        public void buildTable(TableORM table)
        {
            
            string readText = File.ReadAllText(tableTemplate);
            StringBuilder sb = new StringBuilder(readText);

            sb.Replace("@class", table.TableName);
            sb.Replace("@body", buildBody(table.collumn));

            writeToFile(table.TableName, sb.ToString());
        }

        private string buildBody(List<ColumnORM> columns)
        {
            string field = "\t\tpublic @type @name {get; set;}\n";
            StringBuilder body = new StringBuilder();
            foreach(ColumnORM c in columns)
            {
                body.Append(field);
                body.Replace("@type", c.type);
                body.Replace("@name", c.columnName);
            }

            return body.ToString();
        }

        private void writeToFile(string name, string content)
        {
            string path = @"../../output/" + name + ".cs";
            File.WriteAllText(path, content, Encoding.UTF8);
        }
        
    }
}
