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
        static Coffee_Context2 context = null;
        public static Coffee_Context2 Context
        {
            get
            {
                if (context == null)
                {
                    context = new Coffee_Context2();
                }
                return context;
            }
        }
    }

}

