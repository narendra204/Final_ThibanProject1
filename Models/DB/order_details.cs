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
    
    public partial class order_details
    {
        public int id { get; set; }
        public Nullable<int> orderid { get; set; }
        public Nullable<int> productid { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
    
        public virtual order_details order_details1 { get; set; }
        public virtual order_details order_details2 { get; set; }
        public virtual order order { get; set; }
    }
}