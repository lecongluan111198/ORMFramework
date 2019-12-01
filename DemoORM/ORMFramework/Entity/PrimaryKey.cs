using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Entity
{
    class PrimaryKey
    {
        public List<string> columns { get; set; }
        public Dictionary<string, string> references { get; set; } //tablename - columnname

    }
}
