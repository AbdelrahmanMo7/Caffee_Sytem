using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafffe_Sytem.CustomModels
{
    internal class Period_Item
    {
        public int Value { get; set; }
        public string Desc { get; set; }

        public Period_Item() { }
        public Period_Item(int value, string desc)
        {
            Value = value;
            Desc = desc;
        }
    }
}
