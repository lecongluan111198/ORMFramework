using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Entity
{
    class ForeignKey
    {
        public string column { get; set; }
        public KeyValuePair<string, string> reference { get; set; } //tablename
    }
}
