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
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Final_ThibanProject.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();

        // GET: Customer
        [Authorize]
        public ActionResult AddCustomer(int? page, int? pageSizeValue,string  filter,string filterStatus)
        {
            var objCustUser = new List<customeruser>();
            var objFilterUser = new List<customeruser>();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            if (filter == null || filter == "") {
                objCustUser = CallCustomerList();
                ViewBag.statusFilter = filterStatus;
                ViewBag.selectOrderCount = filterStatus;
            }
            else if(filterStatus != null || filterStatus != "")
            {
                objFilterUser = CallCustomerList();
                if(filter == "Status")
                {
                    
                    if(filterStatus == "0" || (filterStatus == "" || filterStatus == ""))
                    {
                        objCustUser = objFilterUser.ToList();
                    }
                    else
                    {
                        objCustUser = objFilterUser.Where(x => x.Status == filterStatus).ToList();
                    }
                    //var objOrderCount = CallCustomerList().ToList();
                    ViewBag.statusFilter = filterStatus;
                    
                }
                else if(filter == "OrderCount")
                {
                    if (filterStatus != "" && filterStatus != "0")
                    {

                        int startCount = Convert.ToInt16(filterStatus.Split('-')[0]);
                        int secondCount = Convert.ToInt16(filterStatus.Split('-')[1]);
                        if(secondCount != 0) { 
                        List<int?> customerID = new List<int?>();
                        customerID = db.orders.GroupBy(x => x.customer_id).Where(grp => grp.Count() > startCount && grp.Count() < secondCount).Select(x => x.Key).ToList();
                        var obj = from a in objFilterUser
                                  join c in customerID on a.customerid equals (c.HasValue ? c.Value : 0)
                                  select a;
                        objCustUser = obj.ToList();
                        }
                        else
                        {
                            List<int?> customerID = new List<int?>();
                            customerID = db.orders.GroupBy(x => x.customer_id).Where(grp => grp.Count() > startCount).Select(x => x.Key).ToList();
                            var obj = from a in objFilterUser
                                      join c in customerID on a.customerid equals (c.HasValue ? c.Value : 0)
                                      select a;
                            objCustUser = obj.ToList();
                        }
                    }
                    else
                    {
                        objCustUser  = CallCustomerList().ToList();
                    }
                    ViewBag.selectOrderCount = filterStatus;
                }
            }
           
           
            return View(objCustUser.ToPagedList(pageNumber, pageSize));
        }



        private List<customeruser> CallCustomerList()
        {
            List<customeruser> objCustomer = new List<customeruser>();

            var CustQuery = (from cust in db.customers
                             where cust.custstatus != "Remove"
                             join addr in db.customerdefaultaddresses on cust.customerid equals addr.custid
                             join cbd in db.customerbankdetails on cust.customerid equals cbd.customerid into ccbd
                             from cbd in ccbd.DefaultIfEmpty()
                             join cap in db.customeraddressproofs on cust.customerid equals cap.customerid into ccap
                             from cap in ccap.DefaultIfEmpty()
                             join ord in db.orders on cust.customerid equals ord.customer_id into cO
                             from ord in cO.DefaultIfEmpty()
                             join img in db.ImageFiles on cust.Image equals img.ImageId into cI
                             from img in cI.DefaultIfEmpty()

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
                                 cust.Image_path,
                                 cbd.account_no,
                                 cbd.bank_name,
                                 cbd.benificary_name_in_bank,
                                 cbd.branch_name,
                                 cbd.ifsc_code,
                                 cap.addressimage
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
                                 status = rc.Key.custstatus,
                                 accountNo = rc.Key.account_no == null ? 0 : rc.Key.account_no,
                                 bankName = rc.Key.bank_name,
                                 benificaryName = rc.Key.benificary_name_in_bank,
                                 branchName = rc.Key.branch_name,
                                 ifscCode = rc.Key.ifsc_code,
                                 addressProf = rc.Key.addressimage
                             }).ToList();

            
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
                    totalspent = item.Totalspent == null ? 0 : item.Totalspent.HasValue ? Math.Round(item.Totalspent.Value, 2) : item.Totalspent,
                    accountNo = item.accountNo,
                    addressProfImage = item.addressProf,
                    bankName = item.bankName,
                    benificary_name = item.benificaryName,
                    branchName = item.branchName,
                    ifscCode = item.ifscCode
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
        public ActionResult AddCustomer(customeruser cu, HttpPostedFileBase file, int? page, int? pageSizeValue, HttpPostedFileBase addressProf)
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
                            string targetPath = Path.Combine(fileName, "customer_" + cu.customerid + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                            string savePath = Path.Combine(targetFolder, "customer_" + cu.customerid + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                            file.SaveAs(savePath);
                            cu.image_path = targetPath;
                        }
                        if (addressProf != null && addressProf.ContentLength > 0)
                        {
                            string fileName = "~/content/images_customer/";
                            string targetFolder = Server.MapPath(fileName);
                            bool exists = System.IO.Directory.Exists(targetFolder);
                            if (!exists)
                            {
                                System.IO.Directory.CreateDirectory(targetFolder);
                            }
                            string targetPath = Path.Combine(fileName, "customer_addressProf" + cu.customerid + addressProf.FileName.Substring(addressProf.FileName.LastIndexOf(".")));
                            string savePath = Path.Combine(targetFolder, "customer_addressProf" + cu.customerid + addressProf.FileName.Substring(addressProf.FileName.LastIndexOf(".")));
                            addressProf.SaveAs(savePath);
                            cu.addressProfImage = targetPath;
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
                            customerbankdetail cdb = new customerbankdetail();
                            cdb.account_no = cu.accountNo;
                            cdb.bank_name = cu.bankName;
                            cdb.branch_name = cu.branchName;
                            cdb.benificary_name_in_bank = cu.benificary_name;
                            cdb.ifsc_code = cu.ifscCode;
                            cdb.customerid = custid;
                            db.customerbankdetails.Add(cdb);
                            db.SaveChanges();
                            if (cu.addressProfImage != null)
                            {
                                customeraddressproof cap = new customeraddressproof();
                                cap.customerid = custid;
                                cap.addressproof = cu.addressProfImage;
                                db.customeraddressproofs.Add(cap);
                                db.SaveChanges();
                            }
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
                            var customerBankDetailCount = db.customerbankdetails.Where(x => x.customerid == cu.customerid).Count();
                            if(customerBankDetailCount > 0)
                            {
                                customerbankdetail ecdb = db.customerbankdetails.First(x => x.customerid == cu.customerid);
                                ecdb.account_no = cu.accountNo;
                                ecdb.bank_name = cu.bankName;
                                ecdb.branch_name = cu.branchName;
                                ecdb.benificary_name_in_bank = cu.benificary_name;
                                ecdb.ifsc_code = cu.ifscCode;
                                db.SaveChanges();
                            }
                            else
                            {
                                customerbankdetail ecdb = new customerbankdetail();
                                ecdb.account_no = cu.accountNo;
                                ecdb.bank_name = cu.bankName;
                                ecdb.branch_name = cu.branchName;
                                ecdb.benificary_name_in_bank = cu.benificary_name;
                                ecdb.ifsc_code = cu.ifscCode;
                                ecdb.customerid = cu.customerid;
                                db.customerbankdetails.Add(ecdb);
                                db.SaveChanges();
                            }
                            if(cu.addressProfImage != null)
                            {
                                var customerAddressProf = db.customeraddressproofs.Where(x => x.customerid == cu.customerid).Count();
                                    if(customerAddressProf > 0)
                                {
                                    customeraddressproof ecap = db.customeraddressproofs.First(x => x.customerid == cu.customerid);
                                    ecap.addressproof = cu.addressProfImage;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    customeraddressproof ecap = new customeraddressproof();
                                    ecap.addressproof = cu.addressProfImage;
                                    ecap.customerid = cu.customerid;
                                    db.customeraddressproofs.Add(ecap);
                                    db.SaveChanges();
                                }
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

        [HttpPost]
        public JsonResult FilterCustomer(int purchaseCount , string status, int? page, int? pageSizeValue)
        {
            JsonResult res = new JsonResult();
            var cu = new List<customeruser>();
            cu = CallCustomerList();
            List<customeruser> obCust = cu.Where(x => x.Status == status).ToList();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            //res = cu.Where(x => x.Status == status).ToList();
            return Json(obCust, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCoustomer(string[] customerid)
        {
            JsonResult res = new JsonResult();
            //res = cu.Where(x => x.Status == status).ToList();
            
            foreach(string i in customerid)
            {
                int cutID = Convert.ToInt16(i);
                db.customers.Find(cutID).custstatus = "Remove";
            }
            db.SaveChanges();
            return Json(true);
        }
    }
}