//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cafffe_Sytem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.Bill_has_Products = new HashSet<Bill_has_Products>();
        }
    
        public int B_ID { get; set; }
        public Nullable<System.DateTime> B_Date { get; set; }
        public Nullable<System.TimeSpan> B_Time { get; set; }
        public double B_Total_Amount { get; set; }
        public int B_Table_Num { get; set; }
        public Nullable<bool> B_IsDeleted_ { get; set; }
        public int Creater_Id { get; set; }
        public Nullable<int> Remover_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_has_Products> Bill_has_Products { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
