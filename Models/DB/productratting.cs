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
    
    public partial class productratting
    {
        public int rateid { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> ratting { get; set; }
        public string comment { get; set; }
        public Nullable<int> orderid { get; set; }
        public Nullable<System.DateTime> ratting_date { get; set; }
    
        public virtual order order { get; set; }
        public virtual product product { get; set; }
    }
}