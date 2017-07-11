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
                             //join ord in db.orders on cust.customerid equals ord.customer_id
                             join addr in db.customerdefaultaddresses on cust.customerid equals addr.custid 
                             join img in db.ImageFiles on cust.Image equals img.ImageId into cI
                             from img in cI.DefaultIfEmpty()
                             group cust by new
                             {
                                 cust.customerid,
                                 cust.mobileno,
                                 cust.name,
                                 cust.emailid,
                                 cust.regdate,
                                 addr.custnote1,
                                 addr.customernote2,
                                 addr.city,
                                 addr.streetaddress,
                                 addr.state,
                                 addr.zip,
                                 addr.country,
                                 img.Imageattachment
                             } into rc 
                             select new
                             {
                                 custid = rc.Key.customerid,
                                 Email = rc.Key.emailid,
                                 Avatar = "",
                                 Name = rc.Key.name,
                                 Mobile = rc.Key.mobileno,
                                 Registered = rc.Key.regdate,
                                 note1 = rc.Key.custnote1,
                                 note2 = rc.Key.customernote2,
                                 address = rc.Key.streetaddress,
                                 city = rc.Key.city,
                                 zip = rc.Key.zip,
                                 state = rc.Key.state,
                                 country = rc.Key.country,
                                 Totalspent ="",
                                 Purchase = ""
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
                    image = { },
                    name = item.Name,
                    mobileno = item.Mobile,
                    emailid = item.Email,
                    custnote1 = item.note1,
                    customernote2 = item.note2,
                    address = item.address,
                    city = item.city,
                    zip = Convert.ToInt32(item.zip),
                    state = item.state,
                    country = item.country,
                    regdate = Convert.ToDateTime(item.Registered),
                    totalpurchase = 0,
                    totalspent = 0
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
                ImageFile obj = new ImageFile();
                int id = 0;
                if (Request.Files.Count > 0)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        using (BinaryReader b = new BinaryReader(file.InputStream))
                        {
                            byte[] binData = b.ReadBytes(file.ContentLength);
                            obj.Imageattachment = binData;
                            obj.ImageName = file.FileName;
                            obj.ImageSize = file.ContentLength;
                            db.ImageFiles.Add(obj);
                            db.SaveChanges();
                            id = obj.ImageId;
                            cust.Image = id;
                        }
                    }
                    cust.regdate = DateTime.Now;
                    cust.name = cu.name;
                    cust.emailid = cu.emailid;
                    cust.password = cu.password;
                    cust.customer_type = cu.customer_type;
                    cust.mobileno = cu.mobileno;
                    cust.createdby = Convert.ToInt32(Session["Adminid"]);
                    cust.custstatus = "Pending";
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
                else
                {
                    ModelState.AddModelError("test", "Customer Already Added.");
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