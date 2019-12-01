using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoORM.Entity
{
    class TableORM
    {
        public string TableName { get; set; }
        public List<ColumnORM> collumn { get; set; }
        public PrimaryORM primaryKey { get; set; }
        public List<ForeignORM> foreignKeys { get; set; }
    }
}
