using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using System.IO;
using PagedList;
using System.Data.Entity.Validation;

namespace Final_ThibanProject.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();

        // GET: Customer
        [Authorize]
        public ActionResult AddCustomer(int? page, int? pageSizeValue)
        {
            var objCustUser = new List<customeruser>();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            objCustUser = CallCustomerList();
            return View(objCustUser.ToPagedList(pageNumber, pageSize));
        }



        private List<customeruser> CallCustomerList()
        {
            List<customeruser> objCustomer = new List<customeruser>();

            var CustQuery = (from cust in db.customers
                             where cust.custstatus != "Remove"
                             join addr in db.customerdefaultaddresses on cust.customerid equals addr.custid
                             join img in db.ImageFiles on cust.Image equals img.ImageId into cI
                             from img in cI.DefaultIfEmpty()
                             join ord in db.orders on cust.customerid equals ord.customer_id into cO
                             from ord in cO.DefaultIfEmpty()
                             group cust by new
                             {
                                 cust.customerid,
                                 cust.mobileno,
                                 cust.name,
                                 cust.emailid,
                                 cust.regdate,
                                 cust.password,
                                 cust.customer_type,
                                 cust.customer_nationality,
                                 cust.custstatus,
                                 addr.custnote1,
                                 addr.customernote2,
                                 addr.city,
                                 addr.streetaddress,
                                 addr.state,
                                 addr.zip,
                                 addr.country,
                                 img.Imageattachment,
                                 ord.total,
                                 ord.quantity,
                                 cust.Image_path
                             } into rc
                             select new
                             {
                                 custid = rc.Key.customerid,
                                 Email = rc.Key.emailid,
                                 Avatar = rc.Key.Imageattachment,
                                 Name = rc.Key.name,
                                 Mobile = rc.Key.mobileno,
                                 Registered = rc.Key.regdate,
                                 Password = rc.Key.password,
                                 CustomerType = rc.Key.customer_type,
                                 Image_path = rc.Key.Image_path,
                                 customerNationality = rc.Key.customer_nationality,
                                 note1 = rc.Key.custnote1,
                                 note2 = rc.Key.customernote2,
                                 address = rc.Key.streetaddress,
                                 city = rc.Key.city,
                                 zip = rc.Key.zip,
                                 state = rc.Key.state,
                                 country = rc.Key.country,
                                 Totalspent = rc.Sum(r => r.orders.Sum(x => x.total)),
                                 Purchase = rc.Sum(r => r.orders.Sum(x => x.quantity)),
                                 status = rc.Key.custstatus
                             }).ToList();

            //var CustQuery = (from cust in db.customers
            //                 join ord in db.orders on cust.customerid equals ord.customer_id
            //                 join addr in db.customerdefaultaddresses on cust.customerid equals addr.custid
            //                 join img in db.ImageFiles on cust.Image equals img.ImageId
            //                 group ord by new
            //                 {
            //                     cust.customerid,
            //                     cust.mobileno,
            //                     cust.name,
            //                     cust.emailid,
            //                     cust.regdate,
            //                     addr.custnote1,
            //                     addr.customernote2,
            //                     addr.city,
            //                     addr.streetaddress,
            //                     addr.state,
            //                     addr.zip,
            //                     addr.country,
            //                     img.Imageattachment
            //                 } into rc
            //                 select new
            //                 {
            //                     custid = rc.Key.customerid,
            //                     Email = rc.Key.emailid,
            //                     Avatar = rc.Key.Imageattachment,
            //                     Name = rc.Key.name,
            //                     Mobile = rc.Key.mobileno,
            //                     Registered = rc.Key.regdate,
            //                     note1 = rc.Key.custnote1,
            //                     note2 = rc.Key.customernote2,
            //                     address = rc.Key.streetaddress,
            //                     city = rc.Key.city,
            //                     zip = rc.Key.zip,
            //                     state = rc.Key.state,
            //                     country = rc.Key.country,
            //                     Totalspent = rc.Sum(r => r.total),
            //                     Purchase = rc.Sum(r => r.quantity)
            //                 }).ToList();
            //foreach (var item in CustQuery)
            //{
            //    objCustomer.Add(new customeruser()
            //    {
            //        customerid = item.custid,
            //        image = item.Avatar,
            //        name = item.Name,
            //        mobileno = item.Mobile,
            //        emailid = item.Email,
            //        custnote1 = item.note1,
            //        customernote2 = item.note2,
            //        address = item.address,
            //        city = item.city,
            //        zip = Convert.ToInt32(item.zip),
            //        state = item.state,
            //        country = item.country,
            //        regdate = Convert.ToDateTime(item.Registered),
            //        totalpurchase = item.Purchase,
            //        totalspent = item.Totalspent.HasValue ? Math.Round(item.Totalspent.Value, 2) : item.Totalspent
            //    });
            //}
            foreach (var item in CustQuery)
            {
                objCustomer.Add(new customeruser()
                {
                    customerid = item.custid,
                    image = item.Avatar,
                    name = item.Name,
                    mobileno = item.Mobile,
                    emailid = item.Email,
                    password = item.Password,
                    customer_type = item.CustomerType,
                    language = item.customerNationality,
                    Status = item.status,
                    image_path = item.Image_path,
                    custnote1 = item.note1,
                    customernote2 = item.note2,
                    address = item.address,
                    city = item.city,
                    zip = Convert.ToInt32(item.zip),
                    state = item.state,
                    country = item.country,
                    regdate = Convert.ToDateTime(item.Registered),
                    totalpurchase = item.Purchase == null ? 0 : item.Purchase,
                    totalspent = item.Totalspent == null ? 0 : item.Totalspent.HasValue ? Math.Round(item.Totalspent.Value, 2) : item.Totalspent
                });
            }
            return objCustomer;
        }


        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }


        [HttpPost]
        public ActionResult AddCustomer(customeruser cu, HttpPostedFileBase file, int? page, int? pageSizeValue)
        {

            try
            {
                customer cust = new customer();
                int id = 0;
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {


                        ImageFile obj = new ImageFile();
                        if (file != null && file.ContentLength > 0)
                        {
                            //using (BinaryReader b = new BinaryReader(file.InputStream))
                            //{
                            //    byte[] binData = b.ReadBytes(file.ContentLength);
                            //    obj.Imageattachment = binData;
                            //    obj.ImageName = file.FileName;
                            //    obj.ImageSize = file.ContentLength;
                            //    db.ImageFiles.Add(obj);
                            //    db.SaveChanges();
                            //    id = obj.ImageId;
                            //    cust.Image = id;
                            //}
                            string fileName = "~/content/images_customer/";
                            string targetFolder = Server.MapPath(fileName);
                            bool exists = System.IO.Directory.Exists(targetFolder);
                            if (!exists)
                            {
                                System.IO.Directory.CreateDirectory(targetFolder);
                            }
                            string targetPath = Path.Combine(fileName, "prod_" + cu.customerid + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                            string savePath = Path.Combine(targetFolder, "prod_" + cu.customerid + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                            file.SaveAs(savePath);
                            cu.image_path = targetPath;
                        }
                        if (cu.customerid == 0)
                        {
                            cust.regdate = DateTime.Now;
                            cust.name = cu.name;
                            cust.emailid = cu.emailid;
                            cust.password = cu.password;
                            cust.customer_type = cu.customer_type;
                            cust.customer_nationality = cu.language;
                            cust.mobileno = cu.mobileno;
                            cust.createdby = Convert.ToInt32(Session["Adminid"]);
                            cust.custstatus = cu.Status == "on" ? "Active" : "Pending";
                            cust.Image_path = cu.image_path;
                            db.customers.Add(cust);
                            db.SaveChanges();
                            var custid = cust.customerid;
                            customerdefaultaddress cda = new customerdefaultaddress();
                            cda.custid = custid;
                            cda.custnote1 = cu.custnote1;
                            cda.customernote2 = cu.customernote2;
                            cda.streetaddress = cu.address;
                            cda.city = cu.city;
                            cda.zip = cu.zip;
                            cda.state = cu.state;
                            cda.country = cu.country;
                            db.customerdefaultaddresses.Add(cda);
                            db.SaveChanges();
                            ViewBag.message = "Customer Added Successfully.";
                        }
                        else if (cu.customerid > 0)
                        {
                            customer c = db.customers.First(i => i.customerid == cu.customerid);
                            c.name = cu.name;
                            c.emailid = cu.emailid;
                            c.password = cu.password;
                            c.customer_type = cu.customer_type;
                            c.customer_nationality = cu.language;
                            c.mobileno = cu.mobileno;
                            c.Image_path = cu.image_path;
                            c.custstatus = cu.Status == "on" ? "Active" : "Pending";
                            db.SaveChanges();
                            var customerDefault = db.customerdefaultaddresses.Where(x => x.custid == cu.customerid).Count();
                            if (customerDefault > 0)
                            {
                                customerdefaultaddress cda = db.customerdefaultaddresses.First(i => i.custid == cu.customerid);
                                cda.streetaddress = cu.address;
                                cda.city = cu.city;
                                cda.zip = cu.zip;
                                cda.country = cu.country;
                                cda.custnote1 = cu.custnote1;
                                cda.customernote2 = cu.customernote2;
                                db.SaveChanges();
                            }
                            else
                            {
                                customerdefaultaddress cda = new customerdefaultaddress();
                                cda.custid = cu.customerid;
                                cda.custnote1 = cu.custnote1;
                                cda.customernote2 = cu.customernote2;
                                cda.streetaddress = cu.address;
                                cda.city = cu.city;
                                cda.zip = cu.zip;
                                cda.state = cu.state;
                                cda.country = cu.country;
                                db.customerdefaultaddresses.Add(cda);
                                db.SaveChanges();
                            }
                            ViewBag.message = "Customer Edited Successfully.";
                        }
                    }
                    else
                    {
                        ViewBag.customerreturn = cu;
                        ViewBag.status = 0;
                        ModelState.AddModelError("test", "Customer Already Added.");
                    }
                }
                else
                {
                    ViewBag.customerreturn = cu;
                    ViewBag.status = 0;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {

                    }
                }
            }
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);

            var objCustUser = new List<customeruser>();
            objCustUser = CallCustomerList();
            return View(objCustUser.ToPagedList(pageNumber, pageSize));
        }



        [HttpPost]
        public JsonResult UpdateStatus(int custId, string sts)
        {
            customer cust = new customer();
            db.customers.Find(custId).custstatus = sts;
            var id = custId;
            cust.custstatus = sts;

            db.SaveChanges();
            return Json(new { customerId = custId, status = sts });
        }

    }
}