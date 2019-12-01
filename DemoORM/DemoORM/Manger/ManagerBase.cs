using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoORM.Manger
{
    class ManagerBase<T, PK>
    {
        public List<T> list()
        {
            return null;
        }

        public void insert(T data)
        {

        }

        public void update(T data)
        {

        }

        public bool delete(PK ID)
        {
            return false;
        }
    }
}
