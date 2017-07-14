using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models
{


    public partial class advertiseList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<int> vender_id { get; set; }
        public byte[] video_image { get; set; }
        public Nullable<bool> status { get; set; }
        public string type { get; set; }

    }
    public partial class CouponList
    {
        public int couponid { get; set; }
        public Nullable<int> venderid { get; set; }
        public string coupon_name { get; set; }
        public string coupon_code { get; set; }
        public Nullable<int> coupon_type { get; set; }
        public Nullable<System.DateTime> coupon_valid_start_date { get; set; }
        public Nullable<System.DateTime> coupon_valid_end_date { get; set; }
        public string coupon_description { get; set; }
        public Nullable<bool> coupon_status { get; set; }
        public Nullable<double> discount { get; set; }
    }

    public partial class CustAllAddresses
    {
        public int customerid { get; set; }
        public virtual customerappartmentaddress_view appart { get; set; }
        public virtual customerchaletaddress_view chaleAddress { get; set; }
    }
    public class customerappartmentaddress_view
    {
        public int id { get; set; }
        public Nullable<int> customerid { get; set; }
        public string appartment_street { get; set; }
        public string appartment_number { get; set; }
        public string floor_number { get; set; }
        public string building_name { get; set; }
    }
    public class customerchaletaddress_view
    {
        public int id { get; set; }
        public Nullable<int> customerid { get; set; }
        public string chalet_street { get; set; }
        public string chalet_number { get; set; }
        public Nullable<bool> chalet_address_status { get; set; }
    }
    public partial class customerdefaultaddress_view
    {
        public int addressid { get; set; }
        public Nullable<int> custid { get; set; }
        public string custnote1 { get; set; }
        public string customernote2 { get; set; }
        public string streetaddress { get; set; }
        public string city { get; set; }
        public Nullable<int> zip { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
    public partial class customerofficeaddress_view
    {
        public int id { get; set; }
        public Nullable<int> customerid { get; set; }
        public string office_street { get; set; }
        public string office_building_name { get; set; }
        public string offie_floor_number { get; set; }
        public string office_number { get; set; }
        public Nullable<bool> office_address_status { get; set; }

    }
    public partial class customerotheraddress_view
    {
        public int id { get; set; }
        public Nullable<int> customerid { get; set; }
        public string other_street { get; set; }
        public string other_specificaqtion { get; set; }
        public Nullable<bool> other_address_status { get; set; }

   }
    public partial class customervillaaddress_view
    {
        public int id { get; set; }
        public Nullable<int> customerid { get; set; }
        public string villa_street { get; set; }
        public string villa_name { get; set; }
        public Nullable<bool> villa_address_status { get; set; }
    }
    public class Product_view
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
      //  public byte[] Image { get; set; }
        public string categoryname { get; set; }
        public int? Availability { get; set; }
        public int? VenderSource { get; set; }
        public string Vendername { get; set; }
        public int? ProductSKU { get; set; }
        public string discount { get; set; }
        public string Status { get; set; }
        public int? Vender_id { get; set; }
     //   public byte[] Vender_Image { get; set; }
        public int? cat_id { get; set; }
        public Nullable<decimal> productprice { get; set; }
        public int? Favourite { get; set; }
        public string Brand { get; set; }
        public string Volume { get; set; }
        public string Material { get; set; }
        public string Image_path { get; set; }
        public string vender_Image { get; set; }
        public int? Stock { get; set; }
        public string ProductAvaibility { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string av_composition_ppm { get; set; }
        public string phno { get; set; }
        public Nullable<int> bottle_per_box { get; set; }

    }
    public partial class order_view
    {
        public int orderid { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public Nullable<int> product_id { get; set; }
        public string product_title { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<int> vender_id { get; set; }

        public string vender_name { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> discount { get; set; }
        public string status { get; set; }
        //Product
        public string prod_image { get; set; }
        public Nullable<System.DateTime> deliverydate { get; set; }
        public string prod_brand { get; set; }
        public string paid_via { get; set; }//payment_type
        //Driver
        public string driver_name { get; set; }
        public string driver_image { get; set; }
        //Vehicle
        public string vehicle_type { get; set; }
        public string plat_no { get; set; }
    }

    public partial class driver_view
    {
        public int driverid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }
        public string dusername { get; set; }
        public string mobile_no { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public Nullable<int> vender_id { get; set; }
        public string driver_nationality { get; set; }
        public string gender { get; set; }
        public string driver_phone_type { get; set; }
        public string driver_divice_id { get; set; }
        public string driver_telicom_carrer { get; set; }
        public string status { get; set; }
        public string Image_path { get; set; }
        public double rating { get; set; }
        public int totalorder { get; set; }
        public int deliverorder { get; set; }
        public int pendingorder { get; set; }
    }
    public class Driver_product_view
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Image_path { get; set; }
        public int? Stock { get; set; }
        public int? OrderQty { get; set; }
        public int? ExtraStock { get; set; }
    }

    public partial class coupon_view
    {
        public int couponid { get; set; }
        public Nullable<int> venderid { get; set; }
        public string coupon_name { get; set; }
        public string coupon_code { get; set; }
        public Nullable<int> coupon_type { get; set; }
        public Nullable<System.DateTime> coupon_valid_start_date { get; set; }
        public Nullable<System.DateTime> coupon_valid_end_date { get; set; }
        public string coupon_description { get; set; }
        public Nullable<bool> coupon_status { get; set; }
        public Nullable<double> discount { get; set; }
        public string condition { get; set; }
        public string vender_name { get; set; }
        public string vender_image { get; set; }
    }
}