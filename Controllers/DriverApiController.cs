using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_ThibanProject_api.Models;
//using Final_ThibanProject.Models;
using Final_ThibanProject.Models.DB;
using System.Data.Entity;
using System.Net.Mail;
using Final_ThibanProject.Models;
using System.Configuration;

namespace Final_ThibanProject_api.Controllers
{
    public class DriverApiController : ApiController
    {
        CustomResponse objR = new CustomResponse();

        [HttpPost]
        [Route]
        public IHttpActionResult GetDriverStockInventory(int driverid, int? shiftid = 1)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Driver_product_view> objProduct = new List<Driver_product_view>();
                var produ_qty_list = (from de in DB.deliveries
                                      join or in DB.orders on de.orderid equals or.orderid
                                      where de.driverid == driverid
                                      group or by new
                                      {
                                          or.product_id
                                      }
                                        into rc
                                      select new
                                      {
                                          ProdId = rc.Key.product_id,
                                          Or_quantity = rc.Sum(or => or.quantity)
                                      }).AsEnumerable();

                var prodid_date = (from drin in DB.driverinventories
                                   where DbFunctions.TruncateTime(drin.date1) == DbFunctions.TruncateTime(DateTime.Now)
                                   && drin.driverid == driverid && drin.shift_id == shiftid
                                   select new { drin.date1, drin.productid, drin.stock }).AsEnumerable();

                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join dr in DB.drivers on ve.venderid equals dr.vender_id
                                    join drin in prodid_date on prod.productid equals drin.productid into drind
                                    from drin in drind.DefaultIfEmpty()
                                        /*  join drin in DB.driverinventories on prod.productid equals drin.productid into drind
                                          from drin in drind.DefaultIfEmpty()*/
                                    join drst in produ_qty_list on prod.productid equals drst.ProdId into drstd
                                    from drst in drstd.DefaultIfEmpty()
                                    where dr.driverid == driverid
                                    && (DbFunctions.TruncateTime(drin.date1) == DbFunctions.TruncateTime(DateTime.Now) || drin.date1 == null)
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.product_title,
                                        prod.Image_path
                                        ,
                                        drin.stock
                                        ,
                                        drst.Or_quantity
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Stock = rc.Key.stock,
                                        Image_path = rc.Key.Image_path
                                        ,
                                        Or_stock = rc.Key.Or_quantity
                                    }).ToList();


                foreach (var item in productQuery)
                {
                    objProduct.Add(new Driver_product_view()
                    {
                        ProductId = item.ProdId,
                        Title = item.ProductTitle,
                        Image_path = item.Image_path,
                        Stock = item.Stock == null ? 0 : item.Stock
                        //   ,OrderQty=item.Or_stock ==null ?0: item.Or_stock
                        // ,ExtraStock=((item.Stock == null ? 0 : item.Stock) - (item.Or_stock == null ? 0 : item.Or_stock))
                    });
                }
                if (objProduct.Count > 0)
                    return Ok(objProduct);
                else
                {
                    objR.status = 1;
                    objR.message = "No Product Found";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);

        }

        [HttpPost]
        [Route]
        public IHttpActionResult GetDriverOrderStockInventory(int driverid, int? shiftid = 1)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Driver_product_view> objProduct = new List<Driver_product_view>();
                var produ_qty_list = (from de in DB.deliveries
                                      join or in DB.orders on de.orderid equals or.orderid
                                      where de.driverid == driverid
                                      group or by new
                                      {
                                          or.product_id
                                      }
                                        into rc
                                      select new
                                      {
                                          ProdId = rc.Key.product_id,
                                          Or_quantity = rc.Sum(or => or.quantity)
                                      }).AsEnumerable();


                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join dr in DB.drivers on ve.venderid equals dr.vender_id
                                    join drst in produ_qty_list on prod.productid equals drst.ProdId
                                    //into drstd from drst in drstd.DefaultIfEmpty()
                                    where dr.driverid == driverid
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.product_title,
                                        prod.Image_path
                                        ,
                                        drst.Or_quantity
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Image_path = rc.Key.Image_path
                                        ,
                                        Or_stock = rc.Key.Or_quantity
                                    }).ToList();


                foreach (var item in productQuery)
                {
                    objProduct.Add(new Driver_product_view()
                    {
                        ProductId = item.ProdId,
                        Title = item.ProductTitle,
                        Image_path = item.Image_path,
                        OrderQty = item.Or_stock == null ? 0 : item.Or_stock
                    });
                }
                if (objProduct.Count > 0)
                    return Ok(objProduct);
                else
                {
                    objR.status = 1;
                    objR.message = "No Product Found";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);

        }

        [HttpPost]
        [Route]
        public IHttpActionResult GetDriverExtraStockInventory(int driverid, int? shiftid = 1)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Driver_product_view> objProduct = new List<Driver_product_view>();
                var produ_qty_list = (from de in DB.deliveries
                                      join or in DB.orders on de.orderid equals or.orderid
                                      where de.driverid == driverid
                                      group or by new
                                      {
                                          or.product_id
                                      }
                                        into rc
                                      select new
                                      {
                                          ProdId = rc.Key.product_id,
                                          Or_quantity = rc.Sum(or => or.quantity)
                                      }).AsEnumerable();

                var prodid_date = (from drin in DB.driverinventories
                                   where DbFunctions.TruncateTime(drin.date1) == DbFunctions.TruncateTime(DateTime.Now)
                                   && drin.driverid == driverid && drin.shift_id == shiftid
                                   select new { drin.date1, drin.productid, drin.stock }).AsEnumerable();

                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join dr in DB.drivers on ve.venderid equals dr.vender_id
                                    join drin in prodid_date on prod.productid equals drin.productid
                                    //       into drind  from drin in drind.DefaultIfEmpty()

                                    join drst in produ_qty_list on drin.productid equals drst.ProdId
                                    into drstd
                                    from drst in drstd.DefaultIfEmpty()
                                    where dr.driverid == driverid && ((drin.stock > drst.Or_quantity) || drst.ProdId == null)
                                    //     && (DbFunctions.TruncateTime(drin.date1) == DbFunctions.TruncateTime(DateTime.Now) || drin.date1 == null)
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.product_title,
                                        prod.Image_path
                                         ,
                                        drin.stock
                                         ,
                                        drst.Or_quantity
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Stock = rc.Key.stock,
                                        Image_path = rc.Key.Image_path
                                        ,
                                        Or_stock = rc.Key.Or_quantity
                                    }).ToList();


                foreach (var item in productQuery)
                {
                    objProduct.Add(new Driver_product_view()
                    {
                        ProductId = item.ProdId,
                        Title = item.ProductTitle,
                        Image_path = item.Image_path,
                        Stock = item.Stock == null ? 0 : item.Stock
                        ,
                        OrderQty = item.Or_stock == null ? 0 : item.Or_stock
                        ,
                        ExtraStock = ((item.Stock == null ? 0 : item.Stock) - (item.Or_stock == null ? 0 : item.Or_stock))
                    });
                }
                if (objProduct.Count > 0)
                    return Ok(objProduct);
                else
                {
                    objR.status = 1;
                    objR.message = "No Product Found";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);

        }

        [HttpPost]
        [Route]
        public IHttpActionResult AddProdcttoDriverInventory(int driverid, int productid, int qty)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                driverinventory driIn = db.driverinventories.Where(x => x.driverid == driverid && x.productid == productid && DbFunctions.TruncateTime(DateTime.Now) == DbFunctions.TruncateTime(x.date1)).FirstOrDefault();
                if (driIn != null)
                {
                    driIn.driverid = driverid;
                    driIn.productid = productid;
                    driIn.stock = qty;
                    driIn.date1 = DateTime.Now;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Stock Updated Successfully";
                }
                else
                {
                    driIn = new driverinventory();
                    driIn.driverid = driverid;
                    driIn.productid = productid;
                    driIn.stock = qty;
                    driIn.date1 = DateTime.Now;
                    db.driverinventories.Add(driIn);
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Stock added Successfully";
                }

            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;
            }
            return Ok(objR);
        }

        [HttpPost]
        [Route]
        public IHttpActionResult DriverLogin(string username, string password)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();

                Final_ThibanProject.Models.DriverDetails objDriverDetails = new Final_ThibanProject.Models.DriverDetails();
                driver vender = db.drivers.Where(x => (x.dusername == username || x.mobile_no == username) && x.password == password && x.status == "1").FirstOrDefault();
                if (vender != null)
                {
                    objDriverDetails.driverid = vender.driverid;
                    objDriverDetails.emailid = vender.emailid ?? "";
                    objDriverDetails.name = vender.name ?? "";
                    objDriverDetails.mobile_no = vender.mobile_no ?? "";
                    objDriverDetails.status = vender.status;
                    objDriverDetails.username = vender.dusername ?? "";
                    return Ok(objDriverDetails);
                }
                else
                {
                    //  return Ok(new { msg = "Invalid username or password." });
                    objR.status = 0;
                    objR.message = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);
        }

        [HttpPost]
        [Route]
        public IHttpActionResult DriverForgotPassword(string username)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                driver driver = db.drivers.Where(x => x.emailid == username && x.status == "1").FirstOrDefault();
                if (driver != null)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(driver.emailid);
                    string fromemail = ConfigurationManager.AppSettings["fromemail"];
                    string frompass = ConfigurationManager.AppSettings["frompass"];
                    string emailhost = ConfigurationManager.AppSettings["emailhost"];
                    int emailport = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
                    string emailssl = ConfigurationManager.AppSettings["emailssl"];

                    mail.From = new MailAddress(fromemail);
                    mail.Subject = "Password change request";

                    string Body = "Hello " + driver.name + ",";
                    Body += "<br /><br />Please find your password";
                    Body += "<br />Password:" + driver.password;
                    Body += "<br /><br />Thanks";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = emailhost;// "smtp.gmail.com";
                    smtp.Port = emailport;// 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(fromemail, frompass);
                    if (emailssl == "1")
                        smtp.EnableSsl = true;
                    smtp.Send(mail);
                    // return Ok(new { msg = "Email sent to your register emailid." });
                    objR.status = 1;
                    objR.message = "Email sent to your register emailid.";
                }
                else
                {
                    //  return Ok(new { msg = "Invalid username." });
                    objR.status = 0;
                    objR.message = "Invalid username.";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);
        }

        [HttpPost]
        [Route]
        public IHttpActionResult GetDriverDetails(int driverid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                double totalrating = Math.Round(DB.driverratings.Where(x => x.driver_id == driverid).Average(x => x.rating).Value, 1);
                int totalorder = DB.deliveries.Where(x => x.driverid == driverid).Count();
                int deliveroder = DB.deliveries.Where(x => x.driverid == driverid && x.deliverytime != null).Count();
                int remainorder = totalorder - deliveroder;
                //  return Ok(DB.drivers.Where(x => x.driverid == driverid).FirstOrDefault());
                List<driver_view> objProduct = new List<driver_view>();
                var driverqry = (from dr in DB.drivers
                                 join ve in DB.venders on dr.vender_id equals ve.venderid
                                 where dr.driverid == driverid
                                 select new
                                 {
                                     dr.driverid,
                                     dr.emailid,
                                     dr.name,
                                     dr.dusername,
                                     dr.mobile_no,
                                     dr.registration_date,
                                     dr.vender_id,
                                     dr.driver_nationality,
                                     dr.gender,
                                     dr.driver_phone_type,
                                     dr.driver_divice_id,
                                     dr.driver_telicom_carrer,
                                     dr.status,
                                     dr.Image_path
                                 }).ToList();
                foreach (var item in driverqry)
                {
                    objProduct.Add(new driver_view()
                    {
                        driverid = item.driverid,
                        emailid = item.emailid,
                        name = item.name,
                        dusername = item.dusername,
                        mobile_no = item.mobile_no,
                        registration_date = item.registration_date,
                        vender_id = item.vender_id,
                        driver_nationality = item.driver_nationality,
                        gender = item.gender,
                        driver_phone_type = item.driver_phone_type,
                        driver_divice_id = item.driver_divice_id,
                        driver_telicom_carrer = item.driver_telicom_carrer,
                        status = item.status,
                        Image_path = item.Image_path,
                        rating = totalrating,
                        totalorder = totalorder,
                        deliverorder = deliveroder,
                        pendingorder = remainorder
                    });
                }
                if (objProduct.Count > 0)
                    return Ok(objProduct);
                else
                {
                    objR.status = 1;
                    objR.message = "No Vender Found";
                }

            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);

        }

        [HttpPost]
        [Route]
        public IHttpActionResult AddOdometerReading(int driverid,  int reading, int? shift_id = 1)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                driver_odometer objdri = new driver_odometer();
                objdri.shift_id = shift_id;
                objdri.driverid = driverid;
                objdri.odometer_start = reading;
                objdri.date1 = DateTime.Now.Date;
                db.driver_odometer.Add(objdri);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Reading added Successfully";
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;
            }
            return Ok(objR);
        }


        #region extra
        [HttpPost]
        [Route]
        public IHttpActionResult GetProductStock(int ProductId)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Product> objProduct = new List<Product>();
                int? productStock = DB.products.Where(x => x.productid == ProductId).Select(x => x.stock).FirstOrDefault();
                if (productStock > 0)
                {
                    // return Ok(productStock);
                    objR.status = 1;
                    objR.message = productStock.ToString();
                }
                else
                {
                    objR.status = 0;
                    objR.message = "Product Not Found";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;

            }
            return Ok(objR);
        }
        #endregion
    }
}
