using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models
{
    public class Coupon
    {
        public int Couponid { get; set; }
        public string couponcode { get; set; }
        public DateTime validstartdate { get; set; }
        public DateTime validenddate { get; set; }
        public bool status { get; set; }
        public Nullable<double> discount { get; set; }
    }
}