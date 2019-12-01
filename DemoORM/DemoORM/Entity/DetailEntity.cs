using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoORM.Entity
{
    class DetailEntity
    {
        public string tableName;
        public Dictionary<DetailEntity, string> referenceEnity;
        public string prinaryKey;
        public int amountOfColumn;
    }
}
