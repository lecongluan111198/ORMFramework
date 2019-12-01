using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoORM.Entity
{
    class PrimaryORM
    {
        public List<ColumnORM> primaryKey { get; set; }
        public Dictionary<string, ColumnORM> foreignKey { get; set; } //tablename - Column
    }
}
