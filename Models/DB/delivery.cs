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
    
    public partial class delivery
    {
        public int deliveryid { get; set; }
        public Nullable<int> driverid { get; set; }
        public Nullable<System.DateTime> deliverystarttime { get; set; }
        public Nullable<decimal> startlongitude { get; set; }
        public Nullable<decimal> startlatitude { get; set; }
        public Nullable<decimal> endlongitude { get; set; }
        public Nullable<decimal> endlatitude { get; set; }
        public Nullable<int> startgeofence { get; set; }
        public Nullable<int> endgeofence { get; set; }
        public Nullable<int> orderid { get; set; }
        public Nullable<System.DateTime> reachedtime { get; set; }
        public Nullable<System.DateTime> deliverytime { get; set; }
        public Nullable<int> weatingtime { get; set; }
        public Nullable<int> delaytime { get; set; }
    
        public virtual driver driver { get; set; }
    }
}
