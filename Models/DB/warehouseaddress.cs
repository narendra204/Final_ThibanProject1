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
    
    public partial class warehouseaddress
    {
        public int warehouseid { get; set; }
        public Nullable<int> venderid { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public Nullable<int> geofenceid { get; set; }
        public Nullable<bool> status { get; set; }
    }
}
