using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafffe_Sytem.A.M.A
{
    public sealed class DBConnection
    {
       
        DBConnection() { }
        static Coffee_SystemEntities  context = null;
        public static Coffee_SystemEntities Context
        {
            get
            {
                if (context == null)
                {
                    context = new Coffee_SystemEntities();
                }
                return context;
            }
        }
    }

}

