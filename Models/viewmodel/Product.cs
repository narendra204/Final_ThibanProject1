using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_ThibanProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage="Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [Required(ErrorMessage="Category Name is required")]
        public string categoryname { get; set; }
        public int Category { get; set; }
        public int? Availability { get; set; }
        public int? VenderSource { get; set; }
        public string Vendername { get; set; }
        public int? ProductSKU { get; set; }
        public string discount { get; set; }
        public string Status { get; set; }
        public byte[] Vender_Image { get; set; }
        public Nullable<decimal> productprice { get; set; }

    }
}