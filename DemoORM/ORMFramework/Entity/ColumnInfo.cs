using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Entity
{
    class ColumnInfo
    {
        public string name { get; set; }
        public string bindingName { get; set; }
        public string DBType { get; set; }
        public string type { get; set; }
    }
}
