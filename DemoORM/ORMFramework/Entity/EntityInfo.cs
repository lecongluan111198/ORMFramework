using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Entity
{
    class EntityInfo
    {
        public string name { get; set; }
        public string bindingName { get; set; }
        public Dictionary<string, ColumnInfo> columns { get; set; } //name - info
        public PrimaryKey primaryKey { get; set; }
        public List<ForeignKey> foreignKeys { get; set; }

        protected void toModel()
        {

        }
    }
}
