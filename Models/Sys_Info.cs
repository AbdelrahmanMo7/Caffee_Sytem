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
    
    public partial class Sys_Info
    {
        public int Sys_Info_ID { get; set; }
        public string Coffee_Name { get; set; }
        public Nullable<long> Coffee_Phone_Number { get; set; }
        public string Coffee_Address { get; set; }
        public string Coffee_Apointment { get; set; }
        public string Coffee_Logo_Path { get; set; }
        public string Coffee_FaceBook_Link { get; set; }
        public string Coffee_Insta_Link { get; set; }
        public Nullable<int> User_Id { get; set; }
    
        public virtual User User { get; set; }
    }
}
