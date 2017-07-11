using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class ProductRating
    {
        public int rateid { get; set; }
        public int? ratting { get; set; }
        public string comment { get; set; }
        public int productid { get; set; }
        public string product_title { get; set; }

        public int venderid { get; set; }
        public string emailid { get; set; }
        public string username { get; set; }
        public byte[] image { get; set; }

    }
}