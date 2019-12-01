using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoORM.Entity
{
    class ColumnORM
    {
        public string columnName { get; set; }
        public string dbType { get; set; }
        public string type { get; set; }
        public bool isPK { get; set; }
        public bool isFK { get; set; }

    }
}
