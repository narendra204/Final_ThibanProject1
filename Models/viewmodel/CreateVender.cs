using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class CreateVender
    {
        public int venderid { get; set; }

        [Required(ErrorMessage = "emailid is required")]
        [EmailAddress]
        public string emailid { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string name { get; set; }


        public string username { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirmpassword { get; set; }

        [Required(ErrorMessage = "mobileno is required")]
        public string mobile_no { get; set; }

        public bool? status { get; set; }
    }

    public class VenderLogin
    {
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }

        public bool? status { get; set; }
    }

    public class VenderDetails
    {
        public int venderid { get; set; }
        public string emailid { get; set; }

        public string image { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string mobile_no { get; set; }
        public string status { get; set; }
        public int Otp { get; set; }
        public DateTime OtpCrtDate { get; set; }

        public int storeid { get; set; }
        // public int? venderid { get; set; }
        public string business_type { get; set; }
        public string language { get; set; }
        public string display_name { get; set; }
        public string reg_business_name { get; set; }
        public string business_address1 { get; set; }
        public string business_address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string warehouse_address1 { get; set; }
        public string warehouse_address2 { get; set; }

        public string Registration_Name { get; set; }

        public string StoreProvince { get; set; }


        public string Bussiness_office_hours { get; set; }

        public string WarehouseProvince { get; set; }

        public string WarehouseCity { get; set; }

    }
    
}