using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ORMFramework.Entity;

namespace ORMFramework.Util
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

        public void buildTable(EntityInfo table)
        {

            string readText = File.ReadAllText(tableTemplate);
            StringBuilder sb = new StringBuilder(readText);

            sb.Replace("@class", table.bindingName);
            sb.Replace("@body", buildBody(table.columns));

            writeToFile(table.bindingName, sb.ToString());
        }

        private string buildBody(Dictionary<string, ColumnInfo> columns)
        {
            string field = "\t\tpublic @type @name {get; set;}\n";
            StringBuilder body = new StringBuilder();
            foreach (KeyValuePair<string, ColumnInfo> c in columns)
            {
                body.Append(field);
                body.Replace("@type", c.Value.type);
                body.Replace("@name", c.Value.bindingName);
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
