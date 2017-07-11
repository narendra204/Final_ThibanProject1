using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using System.IO;
using PagedList;

namespace Final_ThibanProject.Controllers
{
    public class OrderController : Controller
    {

        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        // GET: Order
        [Authorize]
        [HttpGet]
        public ActionResult AddOrder(int? page, int? pageSizeValue)
        {
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            var objdriver = new List<Order>();
            objdriver = CallOrderList();
            return View(objdriver.ToPagedList(pageNumber, pageSize));
        }

        private List<Order> CallOrderList()
        {
            List<Order> objOrder = new List<Order>();
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var OrderQuery = (from ord in db.orders
                                  join add in db.shippingaddresses on ord.orderid equals add.orderid
                                  join cust in db.customers on ord.customer_id equals cust.customerid
                                  join card in db.customerpaymentcards on cust.customerid equals card.customerid
                                  select new
                                  {
                                      add.streetaddress,
                                      add.city,
                                      add.country,
                                      add.zip,
                                      ord.status,
                                      ord.orderid,
                                      ord.orderdate,
                                      ord.expected_delivery_time,
                                      ord.ship_date,
                                      ord.quantity,
                                      cust.name,
                                      cust.emailid,
                                      cust.mobileno,
                                      card.card_number,
                                      card.card_cvv,
                                      card.card_expiry
                                  } into rc
                                  select new
                                  {
                                      orderno = rc.orderid,
                                      purchasedate = rc.orderdate,
                                      deliverytime = rc.ship_date,
                                      Address = rc.streetaddress,
                                      City = rc.city,
                                      Zip = rc.zip,
                                      Country = rc.country,
                                      Quantity = rc.quantity,
                                      Status = rc.status
                                  }).ToList();
                foreach (var item in OrderQuery)
                {
                    objOrder.Add(new Order()
                    {
                        OrderNo = item.orderno,
                        PurchaseDate = Convert.ToDateTime(item.purchasedate),
                        DeliveryTime = Convert.ToDateTime(item.deliverytime),
                        Address = item.Address,
                        Quantity = item.Quantity,
                        City = item.City,
                        Country = item.Country,
                        Zip = Convert.ToInt32(item.Zip),
                        Status = item.Status
                    });
                }
                return objOrder;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOrder(Order od, int? page, int? pageSizeValue)
        {

            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                if (ModelState.IsValid)
                {


                    customer cust = new customer();
                    cust.name = od.FirstName + "" + od.LastName;
                    cust.emailid = od.Email;
                    cust.mobileno = od.Mobile;
                    cust.password = "";
                    db.customers.Add(cust);
                    db.SaveChanges();
                    var id = cust.customerid;
                    order ord = new order();
                    ord.customer_id = id;
                    ord.ship_address = od.Address;
                    ord.quantity = od.Quantity;
                    db.orders.Add(ord);
                    db.SaveChanges();
                    var oid = ord.orderid;


                    customerpaymentcard card = new customerpaymentcard();
                    card.customerid = id;
                    card.card_type = od.CardType;
                    card.card_number = od.CardNumber;
                    card.card_cvv = od.CardCvv;
                    db.customerpaymentcards.Add(card);
                    db.SaveChanges();
                    shippingaddress sd = new shippingaddress();
                    sd.customerid = id;
                    sd.streetaddress = od.Address;
                    sd.city = od.City;
                    sd.zip = od.Zip;
                    sd.state = od.State;
                    sd.country = od.Country;
                    sd.orderid = oid;
                    db.shippingaddresses.Add(sd);
                    db.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("", "");
                }

                var objorder = new List<Order>();
                objorder = CallOrderList();
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                return View(objorder.ToPagedList(pageNumber, pageSize));
            }
        }


        public JsonResult UpdateStatus(int ordId, string sts)
        {
            order ord = new order();
            db.orders.Find(ordId).status = sts;
            var id = ordId;
            ord.status = sts;
            db.SaveChanges();
            return Json(new { orderid = ordId, status = sts });
        }
    }
}