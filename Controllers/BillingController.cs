using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models.viewmodel;

namespace Final_ThibanProject.Controllers
{
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult ProductPayment()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var objproductdetails = new List<ProductPay>();
                objproductdetails = Callproduct();
                return View(objproductdetails);
            }

        }


        private List<ProductPay> Callproduct()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<ProductPay> objproduct = new List<ProductPay>();
                var productquery = (from cust in db.customers
                                    join ord in db.orders on cust.customerid equals ord.customer_id
                                    group ord by new { cust.name, ord.orderid, ord.orderdate, ord.quantity, ord.status }
                                        into rc
                                        select new
                                        {
                                            orderid = rc.Key.orderid,
                                            purchasedate = rc.Key.orderdate,
                                            billtoname = rc.Key.name,
                                            shiptoname = rc.Key.name,
                                            GTbase = rc.Key.quantity,
                                            GTPurchase = rc.Key.quantity,
                                            status = rc.Key.status
                                        }).ToList();
                foreach (var item in productquery)
                {
                    objproduct.Add(new ProductPay()
                        {
                            orderid = item.orderid,
                            purchasedate = Convert.ToDateTime(item.purchasedate),
                            billname = item.billtoname,
                            shipname = item.shiptoname,
                            gtbase = Convert.ToInt32(item.GTbase),
                            gtpurchase = Convert.ToInt32(item.GTPurchase),
                            status = item.status
                        });
                }
                return objproduct;
            }
        }

        public ActionResult ProductBillingDetails(int id)
        {
            var objproductpay = "";
            //using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            //{
            //    ProductPay objproductpay = new ProductPay();
            //    var objtransaction = (from tran in db.prodtransactions
            //                          join ord in db.orders on tran.orderid equals ord.orderid 
            //                         where ord.orderid==id
            //                          select new
            //                          {
            //                              orderid = ord.orderid,
            //                              transactionid = tran.transactionid,
            //                              transactiondate = tran.transactiondate,
            //                              transactionmode = tran.transactionmode,
            //                              transactiontype = tran.transactiontype,
            //                              amount = tran.amount,
            //                              adjamount = tran.adjamount,
            //                              netamount = tran.netamount,
            //                              notes = tran.notes
            //                          }).FirstOrDefault();
            //    if (objtransaction != null)
            //    {
            //        objproductpay.orderid = objtransaction.orderid;
            //        objproductpay.transactionid = objtransaction.transactionid;
            //        objproductpay.transactiondate = Convert.ToDateTime(objtransaction.transactiondate);
            //        objproductpay.transactionmode = objtransaction.transactionmode;
            //        objproductpay.transactiontype = objtransaction.transactiontype;
            //        objproductpay.amount = Convert.ToDouble(objtransaction.amount);
            //        objproductpay.adjamount = Convert.ToDouble(objtransaction.adjamount);
            //        objproductpay.netamount = Convert.ToDouble(objtransaction.netamount);
            //        objproductpay.notes = objtransaction.notes;
            //    }
                return PartialView("_ProductBillingDetails", objproductpay);
            //}
        }
    }
}