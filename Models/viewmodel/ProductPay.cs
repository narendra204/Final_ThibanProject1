using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class ProductPay
    {
        public int orderid { get; set; }
        public DateTime purchasedate { get; set; }
        public string billname { get; set; }
        public string shipname { get; set; }
        public int gtbase { get; set; }
        public int gtpurchase { get; set; }
        public string status { get; set; }
        public string drivername { get; set; }
        public string paymentmethod { get; set; }
        public string benificieryname { get; set; }
        public string requestdetail { get; set; }
        public int transactionid { get; set; }
        public DateTime transactiondate { get; set; }
        public string transactionmode { get; set; }
        public string transactiontype { get; set; }
        public double amount { get; set; }
        public double adjamount { get; set; }
        public double netamount { get; set; }
        public string notes { get; set; }

    }
}