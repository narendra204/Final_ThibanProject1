using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{

    public class VenderProductManagement
    {
        public int productid { get; set; }
        public Nullable<int> vender_id { get; set; }
        public string product_title { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int? category_id { get; set; }
        public decimal? customer_price { get; set; }
        public int? customer_min_order_quantity { get; set; }
        public int? customer_max_order_quantity { get; set; }
        public decimal? store_price { get; set; }
        public int? store_min_order_quantity { get; set; }
        public int? store_max_order_quantity { get; set; }
        public string phno { get; set; }
        public int? bottle_per_box { get; set; }
        public int? stock { get; set; }
        public int? sku { get; set; }
        public bool? status { get; set; }

        public string volume { get; set; }

        public int? AverageCompositionPPM { get; set; }

        public string ProductAvaibility { get; set; }

    }
}