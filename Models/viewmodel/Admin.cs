using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_ThibanProject.Models.DB;

namespace Final_ThibanProject.Models
{
    public class Admin:admin
    {
        public string Male { get; set; }
        public string Female { get; set; }
        public byte[] imagename { get; set; }
    }
}