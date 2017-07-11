using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class ForgotPassword
    {
        public int venderid { get; set; }

        [Required(ErrorMessage = "emailid is required")]
        [EmailAddress]
        public string emailid { get; set; }
        public bool? status { get; set; }
    }


    public class ChangePassword
    {
        public int venderid { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("password")]
        public string ConfirmPassword { get; set; }
    }
}