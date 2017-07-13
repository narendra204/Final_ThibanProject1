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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Newtonsoft;
using System.Web;
using System.Configuration;

namespace Final_ThibanProject_api.Controllers
{
    public class CustomerApiController : ApiController
    {
        CustomResponse objR = new CustomResponse();

        [HttpPost]
        [Route]
        public IHttpActionResult SendOTP(string mobileno)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();


                customer customer = db.customers.Where(x => x.mobileno == mobileno.Trim()).FirstOrDefault();
                if (customer != null)
                {
                    objR.status = 0;
                    objR.message = "Mobileno already exists.";
                }
                else
                {
                    var accountSid = "ACac85a72e4457af58b2aa4eb2b2521915"; //"ACc17f15537340a82993905b683aa5131c";
                    var authToken = "d6979854461568edd5a72425b318890e";// "b73dbc3c5a8d110bb373524b84951ce9";
                    TwilioClient.Init(accountSid, authToken);

                    mobile_otp _objmobile = new mobile_otp();
                    _objmobile.mobileno = mobileno.Trim();
                    Random generator = new Random();
                    String r = generator.Next(0, 1000000).ToString("D6");
                    _objmobile.otp = r;
                    _objmobile.active = true;
                    db.mobile_otp.Add(_objmobile);
                    db.SaveChanges();
                    //   db.mobile_otp.Add()

                    //  MessageResource.
                    //   var otp = MessageResource.

                    mobileno = mobileno.Replace("+", "");
                    mobileno = mobileno.Trim();
                    mobileno = "+" + mobileno;
                    var message = MessageResource.Create(
                        to: new PhoneNumber(mobileno),
                        from: new PhoneNumber("+16507271057"),
                        body: "Dear Customer, Please find the Mobile verification no:" + r);

                    objR.status = 1;
                    objR.message = message.Sid;
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
        public IHttpActionResult OTPVerification(string mobileno, string code)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                // mobileno = "+"+ mobileno;
                mobileno = mobileno.Replace("+", "");
                mobileno = mobileno.Trim();
                mobile_otp otp = db.mobile_otp.Where(x => x.mobileno == mobileno && x.otp == code && x.active == true).OrderByDescending(x => x.id).FirstOrDefault();
                if (otp != null)
                {
                    otp.active = false;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Mobile No Verified Successfully";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "Code is Invalid";
                }
                // return Ok("1");
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
        public IHttpActionResult RegisterCustomer(string name, string email, string password, string mobileno, string gender, string dob)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.emailid == email).FirstOrDefault();
                if (customer != null)
                {
                    objR.status = 0;
                    objR.message = "Email already exists.";
                }
                else
                {
                    customer _objcust = new customer();
                    _objcust.name = name;
                    _objcust.emailid = email;
                    _objcust.password = password;
                    _objcust.customer_gender = gender;
                    _objcust.mobileno = mobileno.Trim();
                    _objcust.DOB = dob;
                    _objcust.custstatus = "Active";
                    db.customers.Add(_objcust);
                    db.SaveChanges();

                    objR.status = 1;
                    objR.message = "Customer is added Successfully";
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
        public IHttpActionResult GetCountryCodeWithFlag()
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                //  string baseurl = "/Contents/flags/";
                return Ok(db.countries.ToList());
                // (x => new CountryFlagDetail { countryid = x.id, code = x.alpha_2, name = x.name, flag = baseurl+x.alpha_2 + ".png" }).ToList());
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
        public IHttpActionResult CustomerLogin(string username, string password)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();

                Final_ThibanProject.Models.CustomerDetail objCustomerDetails = new Final_ThibanProject.Models.CustomerDetail();
                customer customer = db.customers.Where(x => (x.emailid == username || x.mobileno == username) && x.password == password && x.custstatus == "Active").FirstOrDefault();
                if (customer != null)
                {
                    objCustomerDetails.customerid = customer.customerid;
                    objCustomerDetails.emailid = customer.emailid ?? "";
                    objCustomerDetails.name = customer.name ?? "";
                    objCustomerDetails.mobileno = customer.mobileno ?? "";
                    objCustomerDetails.Status = customer.custstatus;
                    // objCustomerDetails.image = customer.Image != null ? customer.ImageFile.Imageattachment : null;
                    objCustomerDetails.gender = customer.customer_gender;
                    objCustomerDetails.DOB = customer.DOB;
                    return Ok(objCustomerDetails);
                }
                else
                {
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
        public IHttpActionResult CustomerForgotPassword(string username)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.emailid == username && x.custstatus == "Active").FirstOrDefault();
                if (customer != null)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(customer.emailid);
                    string fromemail = ConfigurationManager.AppSettings["fromemail"];
                    string frompass = ConfigurationManager.AppSettings["frompass"];
                    string emailhost = ConfigurationManager.AppSettings["emailhost"];
                    int emailport = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
                    string emailssl = ConfigurationManager.AppSettings["emailssl"];

                    mail.From = new MailAddress(fromemail);
                    mail.Subject = "Password change request";

                    string Body = "Hello " + customer.name + ",";
                    Body += "<br /><br />Please find your password";
                    Body += "<br />Password:" + customer.password;
                    Body += "<br /><br />Thanks";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = emailhost;// "relay-hosting.secureserver.net";
                    smtp.Port = emailport;// 25;
                    // smtp.Host = "smtp.gmail.com";//local
                    //smtp.Port = 587;//local
                    if (emailssl == "1")
                        smtp.EnableSsl = true; //local
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (fromemail, frompass);// Enter seders User name and password

                    smtp.Send(mail);
                    objR.status = 1;
                    objR.message = "Email sent to your register emailid.";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "Invalid Username";
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
        public IHttpActionResult GetCMSPageContent(string pagetitle)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();

                //return Ok(db.CmsPages.Select(x => new CountryFlagDetail { countryid = x.id, code = x.alpha_3, name = x.name, flag = x.alpha_2 + ".png" }).ToList());
                CmsPage cmspage = db.CmsPages.Where(x => x.PageTitle == pagetitle).FirstOrDefault();
                return Ok(cmspage);
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
        public IHttpActionResult AddToFavourite(int userid, int productid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerfavoriteproduct _objfav = new customerfavoriteproduct();
                _objfav.customer_id = userid;
                _objfav.product_id = productid;
                db.customerfavoriteproducts.Add(_objfav);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Product Added to Favourite List.";// _objfav.id.ToString();
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
        public IHttpActionResult GetFavouriteList(int custid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product> objProduct = new List<Product>();
                var productQuery = (from fav in DB.customerfavoriteproducts
                                    join prod in DB.products on fav.product_id equals prod.productid
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    where fav.customer_id == custid
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
                                        img.Imageattachment
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status
                    });
                }
                return Ok(objProduct);
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
        public IHttpActionResult GetProductDetail(int productid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product> objProduct = new List<Product>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    where prod.productid == productid
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
                                        img.Imageattachment
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status
                    });
                }
                return Ok(objProduct);
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
        public IHttpActionResult GetBrandwiseProducts(string brand)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product> objProduct = new List<Product>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    where prod.brand == brand
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
                                        img.Imageattachment
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status
                    });
                }
                return Ok(objProduct);
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
        public IHttpActionResult GetProductSearch(string qry)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product> objProduct = new List<Product>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                    where (prod.brand.Contains(qry) || prod.product_title.Contains(qry) || prod.description.Contains(qry) || prod.volume.Contains(qry))
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
                                        img.Imageattachment
                                    }
                                    into rc
                                    from ve in rc.DefaultIfEmpty()
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Imageattachment,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status
                    });
                }
                return Ok(objProduct);
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
        public IHttpActionResult GetProductbyFilter(int? price_order = 0, int? brandid = 0, int? volumeid = 0, string material = "")
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //   if (brandid == null)
                //    brandid = 0;
                string brand = DB.BrandNames.Where(x => x.Id == brandid).Select(x => x.Value).FirstOrDefault();
                string volume = DB.Volumes.Where(x => x.Id == volumeid).Select(x => x.Value).FirstOrDefault();
                // string whe = "";
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product_view> objProduct = new List<Product_view>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                        //where + whe
                                        // where (prod.brand.Contains(qry) || prod.product_title.Contains(qry) || prod.description.Contains(qry) || prod.volume.Contains(qry))
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
                                        prod.Image_path,
                                        prod.brand,
                                        prod.volume,
                                        prod.bottle_material
                                    }
                                    into rc
                                    from ve in rc.DefaultIfEmpty()
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Image_path,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        categoryname = rc.Key.category_name,
                                        Vendername = rc.Key.name,
                                        brand = rc.Key.brand,
                                        material = rc.Key.bottle_material,
                                        volume = rc.Key.volume
                                    }).ToList();
                //Codition check
                if (price_order == 0)
                    productQuery = productQuery.OrderBy(prod => prod.ProdPrice).ToList();
                else
                    productQuery = productQuery.OrderByDescending(prod => prod.ProdPrice).ToList();
                if (brandid > 0 && brandid != 0)
                {
                    productQuery = productQuery.Where(prod => prod.brand == brand).ToList();
                }
                if (volumeid > 0 && volumeid != 0)
                {
                    productQuery = productQuery.Where(prod => prod.volume == volume).ToList();
                }
                if (material != "")
                {
                    productQuery = productQuery.Where(prod => prod.material == material).ToList();
                }
                //End Condition Check
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product_view()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        Image_path = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status,
                        Material = item.material,
                        Volume = item.volume,
                        Brand = item.brand
                    });
                }
                if (objProduct.Count > 0)
                    return Ok(objProduct);
                else
                {
                    objR.status = 1;
                    objR.message = "No Product Found for given criteria";
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
        public IHttpActionResult UpdateUsername(int userid, string newusername)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.customerid == userid).FirstOrDefault();
                if (customer != null)
                {
                    customer.name = newusername;
                    db.SaveChanges();

                    objR.status = 1;
                    objR.message = "UserName changed Successfully.";
                }
                else
                {
                    objR.status = 1;
                    objR.message = "No User Found";
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
        public IHttpActionResult UpdateEmail(int userid, string newemail)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.customerid == userid).FirstOrDefault();
                if (customer != null)
                {
                    customer.emailid = newemail;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Email changed Successfully.";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "User Id is not exist.";
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
        public IHttpActionResult UpdatePassword(int userid, string newpass)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.customerid == userid).FirstOrDefault();
                if (customer != null)
                {
                    customer.password = newpass;
                    db.SaveChanges();

                    MailMessage mail = new MailMessage();
                    mail.To.Add(customer.emailid);
                    string fromemail = ConfigurationManager.AppSettings["fromemail"];
                    string frompass = ConfigurationManager.AppSettings["frompass"];
                    string emailhost = ConfigurationManager.AppSettings["emailhost"];
                    int emailport = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
                    string emailssl = ConfigurationManager.AppSettings["emailssl"];

                    mail.From = new MailAddress(fromemail);
                    mail.Subject = "Password change request";

                    string Body = "Hello " + customer.name + ",";
                    Body += "<br /><br />Please find your password";
                    Body += "<br />Password:" + customer.password;
                    Body += "<br /><br />Thanks";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = emailhost;
                    smtp.Port = emailport;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(fromemail, frompass);
                    // ("test1.igex@gmail.com", "IgexSol12@428");// Enter seders User name and password
                    if (emailssl == "1")
                        smtp.EnableSsl = true;
                    smtp.Send(mail);
                    objR.status = 1;
                    objR.message = "Password changed Successfully.";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "User Id is not exist.";
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
        public IHttpActionResult UpdateGender(int userid, string newgender)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.customerid == userid).FirstOrDefault();
                if (customer != null)
                {
                    customer.customer_gender = newgender;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Gender changed Successfully.";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "User Id is not exist.";
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
        public IHttpActionResult UpdateDOB(int userid, string DOB)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.customerid == userid).FirstOrDefault();
                if (customer != null)
                {
                    customer.DOB = DOB;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Birth Year changed Successfully.";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "User Id is not exist.";
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
        public IHttpActionResult UpdateProfilePic(int userid, string imagepath)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customer customer = db.customers.Where(x => x.customerid == userid).FirstOrDefault();
                if (customer != null)
                {
                    customer.Image_path = imagepath;
                    db.SaveChanges();

                    objR.status = 1;
                    objR.message = "User Photo changed Successfully.";
                }
                else
                {
                    objR.status = 1;
                    objR.message = "No User Found";
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
        public IHttpActionResult GetBrandList()
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                //   BrandList objbrand = new BrandList();
                //  vender vender = db.venders.Where(x => x.status == "Active").Select(.ToList();

                var res = from v in db.venders
                          where v.status == "Active"
                          select new { v.venderid, v.name };
                List<BrandList> list = res.AsEnumerable()
                              .Select(o => new BrandList
                              {
                                  venderid = o.venderid,
                                  name = o.name
                              }).ToList();
                return Ok(list);
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
        public IHttpActionResult GetVolumeList()
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                return Ok(db.Volumes.ToList());
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
        public IHttpActionResult GetMaterialList()
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                return Ok(db.products.Where(x => x.bottle_material != null).Select(x => x.bottle_material).Distinct().ToList());
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
        public IHttpActionResult GetOffers()
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.advertisements
                          where v.status == true
                          select new { v.id, v.name, v.description, v.vender_id, v.video_image, v.status, v.type };


                List<advertiseList> list = res.AsEnumerable()
                                      .Select(o => new advertiseList
                                      {
                                          id = o.id,
                                          name = o.name,
                                          description = o.description,
                                          vender_id = o.vender_id,
                                          video_image = o.video_image,
                                          status = o.status,
                                          type = o.type
                                      }).ToList();
                return Ok(list);

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
        public IHttpActionResult ApplyCoupon(int venderid, string coupon)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.coupons
                          where v.venderid == venderid && v.coupon_code == coupon
                          select new
                          {
                              v.couponid,
                              v.venderid,
                              v.coupon_name,
                              v.coupon_code,
                              v.coupon_type,
                              v.coupon_valid_start_date,
                              v.coupon_valid_end_date,
                              v.coupon_description,
                              v.coupon_status,
                              v.discount
                          };

                List<CouponList> list = res.AsEnumerable()
                                              .Select(o => new CouponList
                                              {
                                                  couponid = o.couponid,
                                                  venderid = o.venderid,
                                                  coupon_name = o.coupon_name,
                                                  coupon_code = o.coupon_code,
                                                  coupon_type = o.coupon_type,
                                                  coupon_valid_start_date = o.coupon_valid_start_date,
                                                  coupon_valid_end_date = o.coupon_valid_end_date,
                                                  coupon_description = o.coupon_description,
                                                  coupon_status = o.coupon_status,
                                                  discount = o.discount
                                              }).ToList();
                if (list.Count > 0)
                {
                    return Ok(list);
                }
                else
                {
                    objR.status = 0;
                    objR.message = "Coupon Code is not Valid.";
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
        public IHttpActionResult GetProductbyCityandState(int custid, string city, string state)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product_view> objProduct = new List<Product_view>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join vadd in DB.warehouseaddresses on ve.venderid equals vadd.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                        //join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                        //from img in imgd.DefaultIfEmpty()
                                        //join img_v in DB.ImageFiles on ve.image equals img_v.ImageId into imgd_v
                                        //from img_v in imgd_v.DefaultIfEmpty()
                                    join fav in DB.customerfavoriteproducts on prod.productid equals fav.product_id into favd
                                    from fav in favd.DefaultIfEmpty()
                                    where vadd.city == city && vadd.state == state && prod.status != "Delete"
                                    //&& (fav.customer_id==custid || fav.customer_id ==null)
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.product_title,
                                        prod.description,
                                        prod.Image_path,
                                        prod.customer_price,
                                        prod.sku,
                                        prod.stock,
                                        prod.status,
                                        prod.category_id,
                                        ca.category_name,
                                        prod.vender_id,
                                        ve.name,
                                        vender_img = ve.Image_path,
                                        prod.discount,
                                        fav.customer_id //(fav.customer_id != null) ? 1: 0 as custid
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Image_path,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        cat_id = rc.Key.category_id,
                                        categoryname = rc.Key.category_name,
                                        Vender_id = rc.Key.vender_id,
                                        Vendername = rc.Key.name,
                                        Vender_Image = rc.Key.vender_img,
                                        discount = rc.Key.discount,
                                        Favourite = rc.Key.customer_id
                                    }).ToList();
                productQuery = productQuery.Where(x => x.Favourite == custid || x.Favourite == null).ToList();// || x=>x.Favourite == null);
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product_view()
                    {
                        ProductId = item.ProdId,
                        Title = item.ProductTitle,
                        Description = item.Description,
                        Image_path = item.Image,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        ProductSKU = item.SKU,
                        Status = item.Status,
                        cat_id = item.cat_id,
                        categoryname = item.categoryname,
                        Vender_id = item.Vender_id,
                        Vendername = item.Vendername,
                        vender_Image = item.Vender_Image,
                        discount = item.discount,
                        Favourite = item.Favourite == custid ? 1 : 0,
                        Availability = item.Stock

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
        public IHttpActionResult GetAllProucts()
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<Product_view> objProduct = new List<Product_view>();
                var productQuery = (from prod in DB.products
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    // join vadd in DB.warehouseaddresses on ve.venderid equals vadd.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid into cad
                                    from ca in cad.DefaultIfEmpty()
                                        //     join fav in DB.customerfavoriteproducts on prod.productid equals fav.product_id into favd
                                        //   from fav in favd.DefaultIfEmpty()
                                    where prod.status != "Delete"
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.product_title,
                                        prod.description,
                                        prod.Image_path,
                                        prod.customer_price,
                                        prod.sku,
                                        prod.stock,
                                        prod.status,
                                        prod.category_id,
                                        ca.category_name,
                                        prod.vender_id,
                                        ve.name,
                                        vender_img = ve.Image_path,
                                        prod.discount
                                        //,fav.customer_id
                                    }
                                        into rc
                                    select new
                                    {
                                        ProdId = rc.Key.productid,
                                        ProductTitle = rc.Key.product_title,
                                        Description = rc.Key.description,
                                        Image = rc.Key.Image_path,
                                        ProdPrice = rc.Key.customer_price,
                                        SKU = rc.Key.sku,
                                        Stock = rc.Key.stock,
                                        Status = rc.Key.status,
                                        cat_id = rc.Key.category_id,
                                        categoryname = rc.Key.category_name,
                                        Vender_id = rc.Key.vender_id,
                                        Vendername = rc.Key.name,
                                        Vender_Image = rc.Key.vender_img,
                                        discount = rc.Key.discount//, Favourite = rc.Key.customer_id
                                    }).ToList();
                //  productQuery = productQuery.Where(x => x.Favourite == custid || x.Favourite == null).ToList();// || x=>x.Favourite == null);
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product_view()
                    {
                        ProductId = item.ProdId,
                        Title = item.ProductTitle,
                        Description = item.Description,
                        Image_path = item.Image,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        ProductSKU = item.SKU,
                        Status = item.Status,
                        cat_id = item.cat_id,
                        categoryname = item.categoryname,
                        Vender_id = item.Vender_id,
                        Vendername = item.Vendername,
                        vender_Image = item.Vender_Image,
                        discount = item.discount,
                        //  Favourite = item.Favourite,// == custid ? 1 : 0,
                        Availability = item.Stock

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
        public IHttpActionResult GetRecentOrderLocation(int custid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                string add_type = DB.orders.Where(x => x.customer_id == custid).OrderByDescending(x => x.orderid).Select(x => x.address_type).FirstOrDefault();
                //6 Address Types
                //appartmentaddress, chaletaddress, defaultaddress, officeaddress, otheraddress, villaaddress
                if (add_type != "" && add_type != null)
                {
                    if (add_type == "appartmentaddress")
                    {
                        var res = from v in DB.customerappartmentaddresses
                                  where v.customerid == custid
                                  select v;
                        List<customerappartmentaddress_view> list = res.AsEnumerable()
                                      .Select(o => new customerappartmentaddress_view
                                      {
                                          appartment_number = o.appartment_number,
                                          appartment_street = o.appartment_street,
                                          building_name = o.building_name,
                                          customerid = o.customerid,
                                          floor_number = o.floor_number,
                                          id = o.id
                                      }).ToList();
                        if (list.Count > 0)
                        {
                            return Ok(list);
                        }
                        else
                        {
                            objR.status = 1;
                            objR.message = "No Address found in table";
                        }
                    }
                    else if (add_type == "chaletaddress")
                    {
                        var res = from v in DB.customerchaletaddresses
                                  where v.customerid == custid
                                  select v;
                        List<customerchaletaddress_view> list = res.AsEnumerable()
                                      .Select(o => new customerchaletaddress_view
                                      {
                                          id = o.id,
                                          customerid = o.customerid,
                                          chalet_address_status = o.chalet_address_status,
                                          chalet_number = o.chalet_number,
                                          chalet_street = o.chalet_street

                                      }).ToList();

                        if (list.Count > 0)
                        {
                            return Ok(list);
                        }
                        else
                        {
                            objR.status = 1;
                            objR.message = "No Address found in table";
                        }
                    }
                    else if (add_type == "defaultaddress")
                    {
                        var res = from v in DB.customerdefaultaddresses
                                  where v.custid == custid
                                  select v;
                        List<customerdefaultaddress_view> list = res.AsEnumerable()
                                      .Select(o => new customerdefaultaddress_view
                                      {
                                          addressid = o.addressid,
                                          custid = o.custid,
                                          city = o.city,
                                          country = o.country,
                                          custnote1 = o.custnote1,
                                          customernote2 = o.customernote2,
                                          state = o.state,
                                          streetaddress = o.streetaddress,
                                          zip = o.zip
                                      }).ToList();
                        if (list.Count > 0)
                        {
                            return Ok(list);
                        }
                        else
                        {
                            objR.status = 1;
                            objR.message = "No Address found in table";
                        }
                    }
                    else if (add_type == "officeaddress")
                    {
                        var res = from v in DB.customerofficeaddresses
                                  where v.customerid == custid
                                  select v;
                        List<customerofficeaddress_view> list = res.AsEnumerable()
                                      .Select(o => new customerofficeaddress_view
                                      {
                                          id = o.id,
                                          customerid = o.customerid,
                                          office_address_status = o.office_address_status,
                                          office_building_name = o.office_building_name,
                                          office_number = o.office_number,
                                          office_street = o.office_street,
                                          offie_floor_number = o.offie_floor_number
                                      }).ToList();
                        if (list.Count > 0)
                        {
                            return Ok(list);
                        }
                        else
                        {
                            objR.status = 1;
                            objR.message = "No Address found in table";
                        }
                    }
                    else if (add_type == "otheraddress")
                    {
                        var res = from v in DB.customerotheraddresses
                                  where v.customerid == custid
                                  select v;
                        List<customerotheraddress_view> list = res.AsEnumerable()
                                      .Select(o => new customerotheraddress_view
                                      {
                                          id = o.id,
                                          customerid = o.customerid,
                                          other_address_status = o.other_address_status,
                                          other_specificaqtion = o.other_specificaqtion,
                                          other_street = o.other_street
                                      }).ToList();
                        if (list.Count > 0)
                        {
                            return Ok(list);
                        }
                        else
                        {
                            objR.status = 1;
                            objR.message = "No Address found in table";
                        }
                    }
                    else if (add_type == "villaaddress")
                    {
                        var res = from v in DB.customervillaaddresses
                                  where v.customerid == custid
                                  select v;
                        List<customervillaaddress_view> list = res.AsEnumerable()
                                      .Select(o => new customervillaaddress_view
                                      {
                                          id = o.id,
                                          customerid = o.customerid,
                                          villa_address_status = o.villa_address_status,
                                          villa_name = o.villa_name,
                                          villa_street = o.villa_street
                                      }).ToList();
                        if (list.Count > 0)
                        {
                            return Ok(list);
                        }
                        else
                        {
                            objR.status = 1;
                            objR.message = "No Address found in table";
                        }
                    }
                }
                else
                {
                    objR.status = 1;
                    objR.message = "No Recent order found for this customer";
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
        public IHttpActionResult GetCustomerOrderList(int custid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                //customerfavoriteproduct _objFav = db.customerfavoriteproducts.Where(x => x.customer_id == userid).Select();
                List<order_view> objProduct = new List<order_view>();
                var productQuery = (from ord in DB.orders
                                    join prod in DB.products on ord.product_id equals prod.productid
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join img in DB.ImageFiles on prod.image equals img.ImageId into imgd
                                    from img in imgd.DefaultIfEmpty()
                                        //   where ord.customer_id=custid
                                    group prod by new
                                    {
                                        ord.customer_id,
                                        ord.orderid,
                                        ord.orderdate,
                                        prod.productid,
                                        prod.product_title,
                                        prod.Image_path,
                                        prod.vender_id,
                                        ve.name,
                                        ord.quantity,
                                        ord.price,
                                        ord.status
                                    }
                                    into rc
                                    select new
                                    {
                                        customer_id = rc.Key.customer_id,
                                        orderid = rc.Key.orderid,
                                        orderdate = rc.Key.orderdate,
                                        productid = rc.Key.productid,
                                        product_title = rc.Key.product_title,
                                        imageattachment = rc.Key.Image_path,
                                        vender_id = rc.Key.vender_id,
                                        vender_name = rc.Key.name,
                                        quantity = rc.Key.quantity,
                                        price = rc.Key.price,
                                        status = rc.Key.status
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new order_view()
                    {
                        orderid = item.orderid,
                        orderdate = item.orderdate,
                        product_id = item.productid,
                        product_title = item.product_title,
                        prod_image = item.imageattachment,
                        vender_id = item.vender_id,
                        vender_name = item.vender_name,
                        quantity = item.quantity,
                        price = item.price,
                        status = item.status,
                        customer_id = item.customer_id
                    });
                }
                if (objProduct.Count > 0)
                {
                    return Ok(objProduct);
                }
                else
                {
                    objR.status = 1;
                    objR.message = "No Order Found";
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
        public IHttpActionResult CancelOrder(int orderid, int custid, string reason)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
                order objorder = DB.orders.Where(x => x.customer_id == custid && x.orderid == orderid
                //checck status later
                ).FirstOrDefault();

                if (objorder != null)
                {
                    objorder.status = "Cancelled";
                    DB.SaveChanges();
                    oredercanclefeedback objfeed = new oredercanclefeedback();
                    objfeed.customer_id = custid;
                    objfeed.order_id = orderid;
                    objfeed.feedback = reason;
                    DB.oredercanclefeedbacks.Add(objfeed);
                    DB.SaveChanges();
                    objR.status = 1;
                    objR.message = "Order cancelled Successfully.";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "Order not fourd or it can't be cancelled";
                }
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;
            }
            return Ok(objR);
        }
        #region Customer Address
        [HttpPost]
        [Route]
        public IHttpActionResult AddCustomerappartmentaddress(int custid, string apt_street, string apt_no, string floor_no, string build_name)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerappartmentaddress objAdd = new customerappartmentaddress();
                objAdd.customerid = custid;
                objAdd.appartment_street = apt_street;
                objAdd.appartment_number = apt_no;
                objAdd.floor_number = floor_no;
                objAdd.building_name = build_name;
                objAdd.building_address_status = true;
                db.customerappartmentaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomerchaletaddress(int custid, string chalet_street, string chalet_number)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerchaletaddress objAdd = new customerchaletaddress();
                objAdd.customerid = custid;
                objAdd.chalet_street = chalet_street;
                objAdd.chalet_number = chalet_number;
                objAdd.chalet_address_status = true;
                db.customerchaletaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomerdefaultaddress(int custid, string custnote1, string customernote2, string streetaddress, string city, int zip, string state, string country)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerdefaultaddress objAdd = new customerdefaultaddress();
                objAdd.custid = custid;
                objAdd.custnote1 = custnote1;
                objAdd.customernote2 = customernote2;
                objAdd.streetaddress = streetaddress;
                objAdd.city = city;
                objAdd.zip = zip;
                objAdd.state = state;
                objAdd.country = country;
                db.customerdefaultaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomerofficeaddress(int custid, string office_street, string office_building_name, string offie_floor_number, string office_number)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerofficeaddress objAdd = new customerofficeaddress();
                objAdd.customerid = custid;
                objAdd.office_street = office_street;
                objAdd.office_building_name = office_building_name;
                objAdd.offie_floor_number = offie_floor_number;
                objAdd.office_number = office_number;
                objAdd.office_address_status = true;
                db.customerofficeaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomerotheraddress(int custid, string other_street, string other_specificaqtion)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerotheraddress objAdd = new customerotheraddress();
                objAdd.customerid = custid;
                objAdd.other_street = other_street;
                objAdd.other_specificaqtion = other_specificaqtion;
                objAdd.other_address_status = true;
                db.customerotheraddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomervillaaddress(int custid, string villa_street, string villa_name)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customervillaaddress objAdd = new customervillaaddress();
                objAdd.customerid = custid;
                objAdd.villa_street = villa_street;
                objAdd.villa_name = villa_name;
                objAdd.villa_address_status = true;
                db.customervillaaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomermosqueaddress(int custid, string street, string mosque_name)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customermosqueaddress objAdd = new customermosqueaddress();
                objAdd.customerid = custid;
                objAdd.street = street;
                objAdd.mosque_name = mosque_name;
                objAdd.status = true;
                db.customermosqueaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult AddCustomerrestaurantaddress(int custid, string rest_street, string floor_no, string rest_no, string rest_name_building_no)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                customerrestaurantaddress objAdd = new customerrestaurantaddress();
                objAdd.customerid = custid;
                objAdd.rest_street = rest_street;
                objAdd.floor_no = floor_no;
                objAdd.rest_no = rest_no;
                objAdd.rest_name_building_no = rest_name_building_no;
                objAdd.status = true;
                db.customerrestaurantaddresses.Add(objAdd);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Address added Successfully";

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
        public IHttpActionResult GetCustomerappartmentaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.customerappartmentaddresses
                          where v.customerid == custid
                          select v;
                List<customerappartmentaddress_view> list = res.AsEnumerable()
                              .Select(o => new customerappartmentaddress_view
                              {
                                  appartment_number = o.appartment_number,
                                  appartment_street = o.appartment_street,
                                  building_name = o.building_name,
                                  customerid = o.customerid,
                                  floor_number = o.floor_number,
                                  id = o.id
                              }).ToList();
                return Ok(list);
                //  return Ok(db.customerappartmentaddresses.Where(x => x.customerid == custid).ToList());
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
        public IHttpActionResult GetCustomerchaletaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.customerchaletaddresses
                          where v.customerid == custid
                          select v;
                List<customerchaletaddress_view> list = res.AsEnumerable()
                              .Select(o => new customerchaletaddress_view
                              {
                                  id = o.id,
                                  customerid = o.customerid,
                                  chalet_address_status = o.chalet_address_status,
                                  chalet_number = o.chalet_number,
                                  chalet_street = o.chalet_street

                              }).ToList();
                return Ok(list);
                //return Ok(db.customerchaletaddresses.Where(x => x.customerid == custid).ToList());
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
        public IHttpActionResult GetCustomerdefaultaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.customerdefaultaddresses
                          where v.custid == custid
                          select v;
                List<customerdefaultaddress_view> list = res.AsEnumerable()
                              .Select(o => new customerdefaultaddress_view
                              {
                                  addressid = o.addressid,
                                  custid = o.custid,
                                  city = o.city,
                                  country = o.country,
                                  custnote1 = o.custnote1,
                                  customernote2 = o.customernote2,
                                  state = o.state,
                                  streetaddress = o.streetaddress,
                                  zip = o.zip
                              }).ToList();
                return Ok(list);
                // return Ok(db.customerdefaultaddresses.Where(x => x.custid == custid).ToList());
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
        public IHttpActionResult GetCustomerofficeaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.customerofficeaddresses
                          where v.customerid == custid
                          select v;
                List<customerofficeaddress_view> list = res.AsEnumerable()
                              .Select(o => new customerofficeaddress_view
                              {
                                  id = o.id,
                                  customerid = o.customerid,
                                  office_address_status = o.office_address_status,
                                  office_building_name = o.office_building_name,
                                  office_number = o.office_number,
                                  office_street = o.office_street,
                                  offie_floor_number = o.offie_floor_number
                              }).ToList();
                return Ok(list);
                // return Ok(db.customerofficeaddresses.Where(x => x.customerid == custid).ToList());
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
        public IHttpActionResult GetCustomerotheraddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.customerotheraddresses
                          where v.customerid == custid
                          select v;
                List<customerotheraddress_view> list = res.AsEnumerable()
                              .Select(o => new customerotheraddress_view
                              {
                                  id = o.id,
                                  customerid = o.customerid,
                                  other_address_status = o.other_address_status,
                                  other_specificaqtion = o.other_specificaqtion,
                                  other_street = o.other_street
                              }).ToList();
                return Ok(list);
                // return Ok(db.customerotheraddresses.Where(x => x.customerid == custid).ToList());
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
        public IHttpActionResult GetCustomervillaaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                var res = from v in db.customervillaaddresses
                          where v.customerid == custid
                          select v;
                List<customervillaaddress_view> list = res.AsEnumerable()
                              .Select(o => new customervillaaddress_view
                              {
                                  id = o.id,
                                  customerid = o.customerid,
                                  villa_address_status = o.villa_address_status,
                                  villa_name = o.villa_name,
                                  villa_street = o.villa_street
                              }).ToList();
                return Ok(list);
                // return Ok(db.customervillaaddresses.Where(x => x.customerid == custid).ToList());
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
        public IHttpActionResult GetCustomermosqueaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                List<customermosqueaddress> list = db.customermosqueaddresses.Where(x => x.customerid == custid).ToList();
                if (list.Count > 0)
                    return Ok(list);
                else
                {
                    objR.status = 1;
                    objR.message = "No Address found";
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
        public IHttpActionResult GetCustomerrestaurantaddress(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                List<customerrestaurantaddress> list = db.customerrestaurantaddresses.Where(x => x.customerid == custid).ToList();
                if (list.Count > 0)
                    return Ok(list);
                else
                {
                    objR.status = 1;
                    objR.message = "No Address found";
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
        public IHttpActionResult GetCustomerDetails(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                CustAllAddresses objCust = new CustAllAddresses();
                return Ok(db.customerappartmentaddresses.Where(x => x.customerid == custid).ToList());
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;
            }
            return Ok(objR);
        }
        #endregion
        #region Notification
        [HttpPost]
        [Route]
        public IHttpActionResult AddCustomer_Notification(int custid, bool from_thiban, bool changes_to_acc, bool coupon_alerts, bool feature_updates)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                cust_notification notifi = db.cust_notification.Where(x => x.customerid == custid).FirstOrDefault();
                if (notifi != null)
                {
                    notifi.customerid = custid;
                    notifi.from_thiban = from_thiban;
                    notifi.changes_to_acc = changes_to_acc;
                    notifi.coupon_alerts = coupon_alerts;
                    notifi.feature_updates = feature_updates;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Notification Setting Updated Successfully";
                }
                else
                {
                    notifi = new cust_notification();
                    notifi.customerid = custid;
                    notifi.from_thiban = from_thiban;
                    notifi.changes_to_acc = changes_to_acc;
                    notifi.coupon_alerts = coupon_alerts;
                    notifi.feature_updates = feature_updates;
                    db.cust_notification.Add(notifi);
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Notification Setting added Successfully";
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
        public IHttpActionResult GetCustNotificationSetting(int custid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                return Ok(db.cust_notification.Where(x => x.customerid == custid).ToList());
            }
            catch (Exception ex)
            {
                objR.status = 0;
                objR.message = ex.Message;
            }
            return Ok(objR);
        }
        #endregion

        [HttpPost]
        [Route]
        public IHttpActionResult GetProductFeedback(int productid)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                //  product_feedback objfeed=(product_feedback) db.product_feedback.Where(x => x.producu_id == productid).ToList(objfeed);
                var res = from v in db.product_feedback
                          where v.producu_id == productid && v.isActive == true
                          select new { v.id, v.producu_id, v.user_id, v.feedback, v.isActive };
                List<product_feedback> list = res.AsEnumerable()
                              .Select(o => new product_feedback
                              {
                                  id = o.id,
                                  user_id = o.user_id,
                                  producu_id = o.producu_id,
                                  feedback = o.feedback,
                                  isActive = o.isActive
                              }).ToList();
                if (list.Count > 0)
                    return Ok(list);
                else
                {
                    objR.status = 1;
                    objR.message = "No feedback for this product";
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
        public IHttpActionResult InsertProductFeedback(int productid, int userid, string feedback)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                product_feedback objfeed = new product_feedback();
                objfeed.user_id = userid;
                objfeed.producu_id = productid;
                objfeed.feedback = feedback;
                objfeed.isActive = true;
                db.product_feedback.Add(objfeed);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "Product feedback added Successfully";


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
        public IHttpActionResult InsertGeneralFeedback(int userid, string feedback)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                General_feeback objfeed = new General_feeback();
                objfeed.customer_id = userid;
                objfeed.feedback = feedback;
                objfeed.isActive = true;
                db.General_feeback.Add(objfeed);
                db.SaveChanges();
                objR.status = 1;
                objR.message = "feedback added Successfully";
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
        public IHttpActionResult GetOrderDetails(int orderid)
        {
            try
            {
                ThibanWaterDBEntities DB = new ThibanWaterDBEntities();

                List<order_view> objProduct = new List<order_view>();
                var driverqry = (from ord in DB.orders
                                 join pro in DB.products on ord.product_id equals pro.productid
                                 join ve in DB.venders on pro.vender_id equals ve.venderid
                                 where ord.orderid == orderid
                                 select new
                                 {
                                     ord.orderid,
                                     ord.orderdate,
                                     ord.product_id,
                                     pro.product_title,
                                     ord.customer_id,
                                     pro.vender_id,
                                     ve.name,
                                     ord.price,
                                     ord.total,
                                     ord.quantity,
                                     ord.discount,
                                     ord.status,
                                     pro.Image_path,
                                     ord.expected_delivery_time,
                                     ord.payment_type
                                 }).ToList();
                /*
      
        //Driver
        public string driver_name { get; set; }
        public string driver_image { get; set; }
        //Vehicle
        public string vehicle_type { get; set; }
        public string plat_no { get; set; }
                 */
                foreach (var item in driverqry)
                {
                    objProduct.Add(new order_view()
                    {
                        orderid = item.orderid,
                        orderdate = item.orderdate,
                        product_id = item.product_id,
                        product_title = item.product_title,
                        customer_id = item.customer_id,
                        vender_id = item.vender_id,
                        vender_name = item.name,
                        price = item.price,
                        total = item.total,
                        quantity = item.quantity,
                        discount = item.discount,
                        status = item.status,
                        prod_image = item.Image_path,
                        deliverydate = item.expected_delivery_time,
                        paid_via = item.payment_type
                    });
                }
                if (objProduct.Count > 0)
                    return Ok(objProduct);
                else
                {
                    objR.status = 1;
                    objR.message = "No Order Found";
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
        public IHttpActionResult UPdateOrderDelivey(int orderid, decimal grandtotal, decimal total, string deliverydate, string address_type, string payment_mode)
        {
            try
            {
                ThibanWaterDBEntities db = new ThibanWaterDBEntities();
                order objOrder = db.orders.Where(x => x.orderid == orderid).FirstOrDefault();
                if (objOrder != null)
                {
                    objOrder.orderid = orderid;
                    objOrder.total = total;
                    objOrder.price = grandtotal;
                    objOrder.ship_date = Convert.ToDateTime(deliverydate);
                    objOrder.address_type = address_type;
                    objOrder.payment_type = payment_mode;
                    db.SaveChanges();
                    objR.status = 1;
                    objR.message = "Order Delivery Updated Successfully";
                }
                else
                {
                    objR.status = 0;
                    objR.message = "No Order Found";
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
