//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cafffe_Sytem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill_has_Products
    {
        public int Bill_ID { get; set; }
        public int Product_ID { get; set; }
        public int Product_Count { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
