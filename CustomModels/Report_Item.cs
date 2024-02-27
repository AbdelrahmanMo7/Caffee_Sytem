using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafffe_Sytem.CustomModels
{
    internal class Report_Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
       
        public Report_Item() { }
        public Report_Item(string Name ,int Count,double Total)
        {
            this.Name = Name;   
            this.Count = Count;
            this.Total = Total;
            this.Price = 0.0;
        }
        public Report_Item(string Name, int Count ,double Price, double Total)
        {
            this.Name = Name;
            this.Count = Count;
            this.Price = Price;
            this.Total = Total;
        }


    }
}
