using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models
{
    public class Order
    {
        public int OrderNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime EstArrivalDate { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CardType { get; set; }
        public int? CardNumber { get; set; }
        public string CardCvv { get; set; }
        public int productid { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }

    }
}