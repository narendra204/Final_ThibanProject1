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
    
    public partial class agreement
    {
        public int AgreementId { get; set; }
        public Nullable<int> VenderId { get; set; }
        public string AgreementVersion { get; set; }
        public Nullable<System.DateTime> AcceptedDate { get; set; }
        public Nullable<int> FileId { get; set; }
    
        public virtual filestore filestore { get; set; }
        public virtual vender vender { get; set; }
    }
}
