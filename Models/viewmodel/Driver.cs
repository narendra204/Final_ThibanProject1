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
        public bool IsDriverMailExist(string Email,int? driverid)
        {
            return db.drivers.Where(a => a.emailid.Equals(Email) && a.driverid!=driverid).Any();
        }
        public int? driverid { get; set; }
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
        public string Nationality { get; set; }
        public int TotalDelivery { get; set; }

        public decimal? Amount { get; set; }
        public string DriverNote1 { get; set; }
        public string DriverNote2 { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        public int Zip { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string deviceID { get; set; }
        public string telecomCarrier { get; set; }
        public string phoneType { get; set; }
        public string image_path { get; set; }

        public int accountNo { get; set; }
        public string benificary_name { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string ifscCode { get; set; }
        public string addressProfImage { get; set; }
    }
}