using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class VenderProduct
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public string brandname { get; set; }
        public string availability { get; set; }
        public string volumn { get; set; }
        public string bottelmaterial { get; set; }
        public int bottleperbox { get; set; }
        public string phno { get; set; }
        public Nullable<decimal> custprice { get; set; }
        public Nullable<decimal> custstoreprice { get; set; }
        public int custminorder { get; set; }
        public int conventionalminorder { get; set; }
        public int custmaxorder { get; set; }
        public int conventionalmaxorder { get; set; }
        public byte[] image { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}