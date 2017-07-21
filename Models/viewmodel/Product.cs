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
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public int? VenderSource { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int? category_id { get; set; }
        public string categoryname { get; set; }
        public string Vendername { get; set; }
        public string brand { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> customer_price { get; set; }
        public int? customer_min_order { get; set; }
        public int? customer_max_order { get; set; }
        public Nullable<decimal> store_price { get; set; }
        public int? store_min_order { get; set; }
        public int? store_max_order { get; set; }
        public string phno { get; set; }
        public int? bottle_per_box { get; set; }
        public int? stock { get; set; }
        [Required(ErrorMessage = "Product SKU required. And only Digits allowed in it.")]
        public int? ProductSKU { get; set; }
        public string Status { get; set; }
        public string availabilityName { get; set; }
        //public byte[] Image { get; set; }
        public int? Availability { get; set; }
        public string Image_path { get; set; }
        public string discount { get; set; }
        public string av_composition_ppm { get; set; }
        public string volume { get; set; }
        public string bottle_material { get; set; }

        public byte[] Image { get; set; }
        public byte[] Vender_Image { get; set; }
        public Nullable<decimal> productprice { get; set; }
    }
}