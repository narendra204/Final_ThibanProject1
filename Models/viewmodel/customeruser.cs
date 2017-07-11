using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_ThibanProject.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace Final_ThibanProject.Models
{
    public class customeruser
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        public bool IsAdminExist(string admin)
        {
            return db.admins.Where(a => a.firstname.Equals(admin)).Any();
        }

        public bool IsAdminMailExist(string Email)
        {
            return db.admins.Where(a => a.email.Equals(Email)).Any();
        }

        public bool IsCustomerExist(string customer)
        {
            return db.customers.Where(a => a.emailid.Equals(customer)).Any();
        }

        public bool IsDriverExist(string driver)
        {
            return db.drivers.Where(a => a.name.Equals(driver)).Any();
        }



        public int customerid { get; set; }
        [Required(ErrorMessage="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage="Email is required")]
        public string emailid { get; set; }
        [Required(ErrorMessage="Password is required")]
        public string password { get; set; }
        public byte[] image { get; set; }
        [Required(ErrorMessage="Mobile No is required")]
        public string mobileno { get; set; }
        public string customer_type { get; set; }
        public string language { get; set; }
        public DateTime regdate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string state { get; set; }
        public string Status { get; set; }
        public string country { get; set; }
        public string custnote1 { get; set; }
        public string customernote2 { get; set; }
        public int? totalpurchase { get; set; }
        public decimal? totalspent { get; set; }
    }

    public class CustomerDetail
    {
        public int customerid { get; set; }
        public string name { get; set; }
        public string emailid { get; set; }
        public string password { get; set; }
        public byte[] image { get; set; }
        public string mobileno { get; set; }
        public string customer_type { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string state { get; set; }
        public string Status { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public string DOB {get;set;}
    }

    public class CountryFlagDetail
    {
        public int countryid { get; set; }
        public string name { get; set; }
        public string flag { get; set; }
        public string code { get; set; }
    }
    public class CustomResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public string errorcode { get; set; }
    }

    public class BrandList
    {
        public int venderid { get; set; }
        public string name { get;set;}
    }
}