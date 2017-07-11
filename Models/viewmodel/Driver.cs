using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_ThibanProject.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace Final_ThibanProject.Models
{
    public class Driver
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        public bool IsDriverMailExist(string Email)
        {
            return db.drivers.Where(a => a.emailid.Equals(Email)).Any();
        }
        public int? Id { get; set; }
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage="Password is required")]
        public string Password { get; set; }
        public byte[] Image { get; set; }
        public DateTime? Regdate { get; set; }
        [Required(ErrorMessage="Mobile No is required")]
        public string Mobile { get; set; }
        public int TotalDelivery { get; set; }
        public decimal? Amount { get; set; }
        public string drivernote1 { get; set; }
        public string drivernote2 { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string country { get; set; }
        public string state { get; set; }

        public string Status { get; set; }
    }

    public class DriverDetails
    {
        public int driverid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string mobile_no { get; set; }
        public string status { get; set; }
    }
}