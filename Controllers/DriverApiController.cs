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
        // public string ProductId { get; set; }
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
        [HttpPost]
        [Route]
        public IHttpActionResult GetOrderProductInventory()
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Product_view> objProduct = new List<Product_view>();
                var productQuery = (from prod in DB.products
                                    join o in DB.orders on prod.productid equals o.product_id
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join dr in DB.drivers on ve.venderid equals dr.vender_id
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.vender_id,
                                        prod.description,
                                        prod.customer_price,
                                        prod.stock,
                                        prod.product_title,
                                        prod.sku,
                                        prod.status,
                                        ca.category_name,
                                     //   img.Imageattachment,
                                        prod.Image_path

                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                       // Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Image_path = rc.Key.Image_path
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product_view()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status,
                        Image_path = item.Image_path
                    });
                }
                if (objProduct.Count > 0)
                {
                    return Ok(objProduct);
                }
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
        public IHttpActionResult GetDriverProductInventory(int driverid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Product_view> objProduct = new List<Product_view>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join dr in DB.drivers on ve.venderid equals dr.vender_id
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    where dr.driverid == driverid
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.vender_id,
                                        prod.description,
                                        prod.customer_price,
                                        prod.stock,
                                        prod.product_title,
                                        prod.sku,
                                        prod.status,
                                        ca.category_name,
                                        ve.name,
                                        img.Imageattachment,
                                        prod.Image_path
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                  //      Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name,
                                        Image_path = rc.Key.Image_path
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product_view()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        //Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status,
                        Image_path = item.Image_path
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
        public IHttpActionResult GetProductInventory()
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<Product_view> objProduct = new List<Product_view>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join dr in DB.drivers on ve.venderid equals dr.vender_id
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.vender_id,
                                        prod.description,
                                        prod.customer_price,
                                        prod.stock,
                                        prod.product_title,
                                        prod.sku,
                                        prod.status,
                                        ca.category_name,
                                        ve.name,
                                        img.Imageattachment,
                                        prod.Image_path
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                      //  Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name,
                                        Image_path = rc.Key.Image_path
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product_view()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                      //  Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status,
                        Image_path = item.Image_path
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

        //post
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

        [HttpPost][Route]
        public IHttpActionResult GetDriverDetails(int driverid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                double totalrating =Math.Round(DB.driverratings.Where(x => x.driver_id == driverid).Average(x=>x.rating).Value,1);
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
                        rating=totalrating
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

    }
}
