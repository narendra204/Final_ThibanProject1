//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_ThibanProject.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class mobile_otp
    {
        public int id { get; set; }
        public string mobileno { get; set; }
        public string otp { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
    }
}
