﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_ThibanProject.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace Final_ThibanProject.Models
{
    public class Vender
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        public bool IsVenderExist(string vender)
        {
            return db.venders.Where(a => a.emailid.Equals(vender)).Any();
        }

        public int Venderid { get; set; }
        [Required(ErrorMessage="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage="Email is required")]
        public string emailid { get; set; }
        [Required(ErrorMessage="Password is required")]
        public string password { get; set; }
        public byte[] image { get; set; }
        [Required(ErrorMessage="Mobile no is required")]
        public string mobileno { get; set; }
        public string companyName { get; set; }
        public string customer_type { get; set; }
        public DateTime? regdate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public decimal? totalsale { get; set; }
        public string vendernote1 { get; set; }
        public string vendernote2 { get; set; }
        public Int32 accountno { get; set; }
        public string benificary_name { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string bankNameCaseOther { get; set; }
        public string ifscCode { get; set; }
        public string HolderName { get; set; }
        public string ibanCode { get; set; }
        public string GSTNo { get; set; }
        public string CSTNo { get; set; }
        public string Status { get; set; }
        public string image_path { get; set; }
    }
}