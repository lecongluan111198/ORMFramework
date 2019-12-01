using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMFramework.Entity;
using ORMFramework.Util;

namespace ORMFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            List<EntityInfo> tables = QueryUtil.INSTANCE.getTables();
            foreach (EntityInfo t in tables)
            {
                Console.WriteLine(t);
                FileUtil.INSTANCE.buildTable(t);
            }
        }
    }
}
