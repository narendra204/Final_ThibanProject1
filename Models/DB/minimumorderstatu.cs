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
    
    public partial class minimumorderstatu
    {
        public int min_order_id { get; set; }
        public Nullable<int> vender_id { get; set; }
        public string order_type { get; set; }
        public Nullable<int> order_quantity { get; set; }
        public Nullable<bool> status { get; set; }
    
        public virtual vender vender { get; set; }
    }
}
