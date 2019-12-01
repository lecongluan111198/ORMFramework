using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoORM.Entity
{
    class ForeignORM
    {
        public ColumnORM foreignKey { get; set; }
        public PrimaryORM primayKey { get; set; }
    }
}
