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
    
    public partial class admin
    {
        public int adminid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string website { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public string mobile { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> Image { get; set; }
        public Nullable<int> roleid { get; set; }
        public string Image_Url { get; set; }
    
        public virtual ImageFile ImageFile { get; set; }
    }
}
