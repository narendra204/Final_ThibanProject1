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
    
    public partial class driverbankdetail
    {
        public int driver_bank_id { get; set; }
        public int accountno { get; set; }
        public Nullable<int> driver_id { get; set; }
        public string benificary_name_in_bank { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string benificary_name_in_bank_image { get; set; }
    
        public virtual driver driver { get; set; }
    }
}
