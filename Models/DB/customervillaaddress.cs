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
    
    public partial class customervillaaddress
    {
        public int id { get; set; }
        public Nullable<int> customerid { get; set; }
        public string villa_street { get; set; }
        public string villa_name { get; set; }
        public Nullable<bool> villa_address_status { get; set; }
    
        public virtual customer customer { get; set; }
    }
}
