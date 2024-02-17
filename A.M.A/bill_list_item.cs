using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafffe_Sytem.A.M.A
{
    // --- custom bill item class-----
    #region custom bill item class
   public class bill_list_item
    {
        public Product Item { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public bill_list_item(Product Item, int count, double total)
        {
            this.Item = Item;
            this.Count = count;
            this.Total = total;
        }
        public override bool Equals(object obj)
        {
            if (obj is Product)
            {
                Product p = obj as Product;
                return this.Item.P_ID == p.P_ID && this.Item.P_Name == p.P_Name;
            }
            if (obj is bill_list_item)
            {
                bill_list_item p = obj as bill_list_item;
                return this.Item.P_ID == p.Item.P_ID && this.Item.P_Name == p.Item.P_Name;
            }
            return false;
        }

    }
    #endregion
    //----------------------
}
