using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models;
using Final_ThibanProject.Models.DB;
using System.IO;
using Final_ThibanProject.Models.LogError;
using Final_ThibanProject.Models.viewmodel;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity;
using System.Web.Security;
using PagedList;
using System.Data.Entity.Validation;
using System.Configuration;

namespace Final_ThibanProject.Controllers
{
    public class VenderController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        LogError objLogError = new LogError();


        // GET: Vender
        public ActionResult AddVender(int? page, int? pageSizeValue, string filter, string filterStatus)
        {
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            var objvenderuser = new List<Vender>();
            objvenderuser = CallVenderList();

            if (filter == null || filter == "")
            {
                ViewBag.filter_status = filterStatus;
                ViewBag.filter_order = filterStatus;
                ViewBag.filter_product = filterStatus;
            }
            else if (filterStatus != null || filterStatus != "")
            {
                //    objFilterProduct = CallProductList();
                if (filter == "Status")
                {

                    if (filterStatus == "All" || (filterStatus == "" || filterStatus == ""))
                    {
                        // objFilterProduct = objProduct.ToList();
                    }
                    else
                    {
                        objvenderuser = objvenderuser.Where(x => x.Status == filterStatus).ToList();
                    }
                    ViewBag.filter_status = filterStatus;
                }
                else if (filter == "OrderCount")
                {
                    //Order Count Filter
                    if (filterStatus != "" && filterStatus != "0")
                    {

                        int startCount = Convert.ToInt16(filterStatus.Split('-')[0]);
                        int secondCount = Convert.ToInt16(filterStatus.Split('-')[1]);
                        if (secondCount != 0)
                        {
                            List<int?> ProductIDs = new List<int?>();
                            ProductIDs = db.orders.GroupBy(x => x.product_id).Where(grp => grp.Count() > startCount && grp.Count() < secondCount).Select(x => x.Key).ToList();
                            var obj = from a in objvenderuser
                                      join pro in db.products on a.Venderid equals pro.vender_id
                                      join p in ProductIDs on pro.productid equals (p.HasValue ? p.Value : 0)
                                      select a;
                            objvenderuser = obj.ToList();
                        }
                        else
                        {
                            List<int?> ProductIDs = new List<int?>();
                            ProductIDs = db.orders.GroupBy(x => x.product_id).Where(grp => grp.Count() > startCount).Select(x => x.Key).ToList();
                            var obj = from a in objvenderuser
                                      join pro in db.products on a.Venderid equals pro.vender_id
                                      join p in ProductIDs on pro.productid equals (p.HasValue ? p.Value : 0)
                                      select a;
                            objvenderuser = obj.ToList();
                        }
                    }
                    ViewBag.filter_order = filterStatus;
                }
                else if (filter == "ProductCount")
                {
                    //Product Count Filter
                    if (filterStatus != "" && filterStatus != "0")
                    {

                        int startCount = Convert.ToInt16(filterStatus.Split('-')[0]);
                        int secondCount = Convert.ToInt16(filterStatus.Split('-')[1]);
                        if (secondCount != 0)
                        {
                            List<int?> ProductIDs = new List<int?>();
                            ProductIDs = db.products.GroupBy(x => x.vender_id).Where(grp => grp.Count() > startCount && grp.Count() < secondCount).Select(x => x.Key).ToList();
                            var obj = from a in objvenderuser
                                      join pro in db.products on a.Venderid equals pro.vender_id
                                      join p in ProductIDs on pro.productid equals (p.HasValue ? p.Value : 0)
                                      select a;
                            objvenderuser = obj.ToList();
                        }
                        else
                        {
                            List<int?> ProductIDs = new List<int?>();
                            ProductIDs = db.products.GroupBy(x => x.vender_id).Where(grp => grp.Count() > startCount).Select(x => x.Key).ToList();
                            var obj = from a in objvenderuser
                                      join pro in db.products on a.Venderid equals pro.vender_id
                                      join p in ProductIDs on pro.productid equals (p.HasValue ? p.Value : 0)
                                      select a;
                            objvenderuser = obj.ToList();
                        }
                    }
                    ViewBag.filter_product = filterStatus;
                }

            }
            return View(objvenderuser.ToPagedList(pageNumber, pageSize));
        }


        private List<Vender> CallVenderList()
        {
            //Fatching data into list object.
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<Vender> objVender = new List<Vender>();
                var VenderQuery = (from v in db.venders
                                   where v.status != "Deleted"
                                   join a in db.venderdefaultaddresses on v.venderid equals a.venderid into vad
                                   from a in vad.DefaultIfEmpty()
                                   join b in db.venderbankdetails on v.venderid equals b.venderid into vvb
                                   from b in vvb.DefaultIfEmpty()
                                   join p in db.products on v.venderid equals p.vender_id into pV
                                   from p in pV.DefaultIfEmpty()
                                   join o in db.orders on p.productid equals o.product_id into oV
                                   from o in oV.DefaultIfEmpty()
                                       /*     join img in db.ImageFiles on v.image equals img.ImageId into oI
                                            from img in oI.DefaultIfEmpty()*/
                                   group o by new
                                   {
                                       v.venderid,
                                       v.name,
                                       v.emailid,
                                       v.registration_date,
                                       v.mobile_no,
                                       v.CompanyName,
                                       v.password,
                                       v.status,
                                       v.Image_path,
                                       a.vendernote1,
                                       a.vendernote2,
                                       a.streetaddress,
                                       a.city,
                                       a.zip,
                                       a.country,
                                       a.state,
                                       b.account_no,
                                       b.bank_name,
                                       b.benificary_name_in_bank,
                                       b.BankNameCaseOther,
                                       b.branch_name,
                                       b.HolderName,
                                       b.IBANCode,
                                       b.ifsc_code,
                                       b.VatIdentityFicationNumber
                                       // img.Imageattachment

                                   } into rc
                                   select new
                                   {
                                       VenderId = rc.Key.venderid,
                                       Email = rc.Key.emailid,
                                       Avatar = rc.Key.Image_path,
                                       Name = rc.Key.name,
                                       password = rc.Key.password,
                                       CompanyName = rc.Key.CompanyName,
                                       confirmPassword = rc.Key.password,
                                       MobileNo = rc.Key.mobile_no,
                                       Register = rc.Key.registration_date,
                                       TotalSale = rc.Sum(r => r.total),
                                       Address = rc.Key.streetaddress,
                                       City = rc.Key.city,
                                       Zip = rc.Key.zip,
                                       Status = rc.Key.status,
                                       venderNote1 = rc.Key.vendernote1,
                                       venderNote2 = rc.Key.vendernote2,
                                       streetaddress = rc.Key.streetaddress,
                                       city = rc.Key.city,
                                       zip = rc.Key.zip,
                                       country = rc.Key.country,
                                       state = rc.Key.state,
                                       accountno = (rc.Key.account_no == null) ? 0 : rc.Key.account_no,
                                       benificary_name = rc.Key.benificary_name_in_bank,
                                       bankName = rc.Key.bank_name,
                                       branchName = rc.Key.branch_name,
                                       bankNameCaseOther = rc.Key.BankNameCaseOther,
                                       ifscCode = rc.Key.ifsc_code,
                                       HolderName = rc.Key.HolderName,
                                       ibanCode = rc.Key.IBANCode,
                                       GSTNo = rc.Key.VatIdentityFicationNumber,
                                       image_path = rc.Key.Image_path
                                   }).ToList();

                //Binding data to a report which is shown on vender view
                foreach (var item in VenderQuery)
                {
                    objVender.Add(new Vender()
                    {
                        Venderid = item.VenderId,
                        //  image = item.Avatar,
                        name = item.Name,
                        emailid = item.Email,
                        regdate = Convert.ToDateTime(item.Register),
                        password = item.password,
                        companyName = item.CompanyName,
                        totalsale = item.TotalSale.HasValue ? Math.Round(item.TotalSale.Value, 2) : item.TotalSale,
                        mobileno = item.MobileNo,
                        address = item.Address,
                        city = item.City,
                        zip = Convert.ToInt32(item.Zip),
                        Status = item.Status,
                        country = item.country,
                        state = item.state,
                        vendernote1 = item.venderNote1,
                        vendernote2 = item.venderNote2,
                        accountno = item.accountno,
                        benificary_name = item.benificary_name,
                        bankName = item.bankName,
                        branchName = item.branchName,
                        bankNameCaseOther = item.bankNameCaseOther,
                        ifscCode = item.ifscCode,
                        HolderName = item.HolderName,
                        ibanCode = item.ibanCode,
                        GSTNo = item.GSTNo,
                        image_path = item.image_path
                    });
                }
                return objVender;
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddVender(Vender ve, HttpPostedFileBase file, int? page, int? pageSizeValue)
        {
            try
            {
                vender ven = new vender();
                using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
                {
                    if (ModelState.IsValid)
                    {

                        ImageFile obj = new ImageFile();
                        int id = 0;
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
                            //    ven.image = id;
                            //}
                            string fileName = "~/content/images_vender/";
                            string targetFolder = Server.MapPath(fileName);
                            bool exists = System.IO.Directory.Exists(targetFolder);
                            if (!exists)
                            {
                                System.IO.Directory.CreateDirectory(targetFolder);
                            }
                            string targetPath = Path.Combine(fileName, "prod_" + ve.Venderid + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                            string savePath = Path.Combine(targetFolder, "prod_" + ve.Venderid + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                            file.SaveAs(savePath);
                            ve.image_path = targetPath;
                        }

                        if (ve.Venderid == 0)
                        {
                            if (!ve.IsVenderExist(ve.emailid))
                            {
                                ven.name = ve.name;
                                ven.emailid = ve.emailid;
                                ven.username = ve.emailid;
                                ven.registration_date = DateTime.Now;
                                ven.password = ve.password;
                                ven.mobile_no = ve.mobileno;
                                ven.Image_path = ve.image_path;
                                ven.CompanyName = ve.companyName;
                                ven.MerchantId = Convert.ToString(32000 + id);
                                ven.status = ve.Status == "on" ? "Active" : "Pending";
                                ven.createdby = Convert.ToInt32(Session["Adminid"]);
                                db.venders.Add(ven);
                                db.SaveChanges();
                                var vid = ven.venderid;
                                venderdefaultaddress vda = new venderdefaultaddress();
                                vda.venderid = vid;
                                vda.vendernote1 = ve.vendernote1;
                                vda.vendernote2 = ve.vendernote2;
                                vda.streetaddress = ve.address;
                                vda.city = ve.city;
                                vda.zip = ve.zip;
                                vda.state = ve.state;
                                vda.country = ve.country;
                                db.venderdefaultaddresses.Add(vda);
                                db.SaveChanges();
                                venderbankdetail vbd = new venderbankdetail();
                                vbd.venderid = vid;
                                vbd.account_no = Convert.ToInt32(ve.accountno);
                                vbd.bank_name = ve.bankName;
                                vbd.benificary_name_in_bank = ve.benificary_name;
                                vbd.branch_name = ve.branchName;
                                vbd.BankNameCaseOther = ve.bankNameCaseOther;
                                vbd.ifsc_code = ve.ifscCode;
                                vbd.HolderName = ve.HolderName;
                                vbd.IBANCode = ve.ibanCode;
                                vbd.VatIdentityFicationNumber = ve.GSTNo;
                                db.venderbankdetails.Add(vbd);
                                db.SaveChanges();
                                ViewBag.message = "Vender Added Sucessfully";
                            }
                            else
                            {
                                ViewBag.clientreturn = ve;
                                ViewBag.status = 0;
                                ModelState.AddModelError("", "Vender Already Exist");
                            }
                        }
                        else if (ve.Venderid > 0)
                        {
                            vender eve = db.venders.First(i => i.venderid == ve.Venderid);
                            eve.name = ve.name;
                            eve.emailid = ve.emailid;
                            eve.username = ve.emailid;
                            eve.registration_date = DateTime.Now;
                            eve.password = ve.password;
                            eve.mobile_no = ve.mobileno;
                            eve.Image_path = ve.image_path;
                            eve.CompanyName = ve.companyName;
                            eve.status = ve.Status == "on" ? "Active" : "Pending";
                            eve.createdby = Convert.ToInt32(Session["Adminid"]);
                            db.SaveChanges();
                            var venderDefaultAdd = db.venderdefaultaddresses.Where(x => x.venderid == ve.Venderid).Count();
                            if (venderDefaultAdd > 0)
                            {
                                venderdefaultaddress evda = db.venderdefaultaddresses.First(v => v.venderid == ve.Venderid);
                                evda.vendernote1 = ve.vendernote1;
                                evda.vendernote2 = ve.vendernote2;
                                evda.streetaddress = ve.address;
                                evda.city = ve.city;
                                evda.zip = ve.zip;
                                evda.state = ve.state;
                                evda.country = ve.country;
                                db.SaveChanges();
                            }
                            else
                            {
                                venderdefaultaddress vda = new venderdefaultaddress();
                                vda.venderid = ve.Venderid;
                                vda.vendernote1 = ve.vendernote1;
                                vda.vendernote2 = ve.vendernote2;
                                vda.streetaddress = ve.address;
                                vda.city = ve.city;
                                vda.zip = ve.zip;
                                vda.state = ve.state;
                                vda.country = ve.country;
                                db.venderdefaultaddresses.Add(vda);
                                db.SaveChanges();
                            }

                            var venderDefaultBank = db.venderbankdetails.Where(x => x.venderid == ve.Venderid).Count();
                            if (venderDefaultBank > 0)
                            {
                                venderbankdetail evbd = db.venderbankdetails.First(e => e.venderid == ve.Venderid);
                                evbd.account_no = Convert.ToInt32(ve.accountno);
                                evbd.bank_name = ve.bankName;
                                evbd.benificary_name_in_bank = ve.benificary_name;
                                evbd.branch_name = ve.branchName;
                                evbd.BankNameCaseOther = ve.bankNameCaseOther;
                                evbd.ifsc_code = ve.ifscCode;
                                evbd.HolderName = ve.HolderName;
                                evbd.IBANCode = ve.ibanCode;
                                evbd.VatIdentityFicationNumber = ve.GSTNo;
                                db.SaveChanges();
                            }
                            else
                            {
                                venderbankdetail vbd = new venderbankdetail();
                                vbd.venderid = ve.Venderid;
                                vbd.account_no = Convert.ToInt32(ve.accountno);
                                vbd.bank_name = ve.bankName;
                                vbd.benificary_name_in_bank = ve.benificary_name;
                                vbd.branch_name = ve.branchName;
                                vbd.BankNameCaseOther = ve.bankNameCaseOther;
                                vbd.ifsc_code = ve.ifscCode;
                                vbd.HolderName = ve.HolderName;
                                vbd.IBANCode = ve.ibanCode;
                                vbd.VatIdentityFicationNumber = ve.GSTNo;
                                db.venderbankdetails.Add(vbd);
                                db.SaveChanges();
                            }
                            ViewBag.message = "Vender Updated Sucessfully";
                        }

                    }


                    else
                    {
                        ViewBag.clientreturn = ve;
                        ViewBag.status = 0;
                    }
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
            var objvenuser = new List<Vender>();
            objvenuser = CallVenderList();
            //////////
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            return View(objvenuser.ToPagedList(pageNumber, pageSize));
        }


        public JsonResult UpdateStatus(int venId, string sts)
        {
            vender ven = new vender();
            ////activation mail
            Guid activationCode = Guid.NewGuid();
            if (sts == "Approved")
            {
                vender vender = db.venders.Where(a => a.venderid == venId).FirstOrDefault();
                if (vender != null)
                {
                    vender.status = sts;
                    vender.Activationid = activationCode;
                    db.Entry(vender).State = EntityState.Modified;
                }

                db.SaveChanges();
                //activation mail
                string fromemail = ConfigurationManager.AppSettings["fromemail"];
                string frompass = ConfigurationManager.AppSettings["frompass"];
                string emailhost = ConfigurationManager.AppSettings["emailhost"];
                int emailport = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
                string emailssl = ConfigurationManager.AppSettings["emailssl"];

                MailMessage mail = new MailMessage();
                mail.To.Add(vender.emailid);
                mail.From = new MailAddress(fromemail);
                mail.Subject = "Activation Mail";
                //string Body = "Hello, " + vender.name + "\n Your Account is activated please click  the  link  to login \n http://localhost:56348/login ";
                string Body = "Hello " + vender.name + ",";
                Body += "<br /><br />Please click the following link to activate your account";
                Body += "<br /><a href = '" + string.Format("{0}://{1}/Vender/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                Body += "<br /><br />Thanks";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = emailhost;// "smtp.gmail.com";
                smtp.Port = emailport;// 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(fromemail, frompass);
                // ("marlusybanch@gmail.com", "yogita@09");// Enter seders User name and password
                if (emailssl == "1")
                    smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            else
            {
                db.venders.Find(venId).status = sts;
                // var id = venId;
                ven.status = sts;
                db.SaveChanges();
            }
            //end activation mail
            ////
            //db.venders.Find(venId).status = sts;
            //var id = venId;
            //ven.status = sts;
            //db.SaveChanges();
            return Json(new { venderid = venId, status = sts });
        }

        [HttpPost]
        public JsonResult DeleteVender(string[] ids)
        {
            JsonResult res = new JsonResult();
            if (ids != null && ids.Length > 0)
            {
                foreach (string i in ids)
                {
                    int id = Convert.ToInt16(i);
                    db.venders.Find(id).status = "Deleted";
                }
                db.SaveChanges();
            }
            return Json(true);

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateVender()
        {
            CreateVender obj = new CreateVender();
            return View(obj);
        }


        [HttpPost]
        public ActionResult CreateVender(CreateVender obj)
        {
            string data = "false";
            vender objCreateVender = new vender();
            try
            {
                if (ModelState.IsValid)
                {
                    List<vender> vender = db.venders.Where(a => a.emailid != null && a.emailid != "").ToList();
                    if (vender != null)
                    {

                        foreach (var A in vender)
                        {
                            if (A.emailid == obj.emailid)
                            {
                                data = "true";
                                break;
                            }

                        }


                    }
                    if (data != "true")
                    {

                        objCreateVender.emailid = obj.emailid ?? "";
                        objCreateVender.mobile_no = obj.mobile_no ?? "";
                        objCreateVender.CompanyName = obj.name ?? "";
                        objCreateVender.name = "";
                        objCreateVender.password = obj.password ?? "";
                        objCreateVender.status = "Pending";
                        objCreateVender.Accepted = false;
                        objCreateVender.username = obj.emailid ?? "";
                        db.venders.Add(objCreateVender);
                        db.SaveChanges();
                        int id = objCreateVender.venderid;

                        objCreateVender.MerchantId = Convert.ToString(32000 + id);
                        db.Entry(objCreateVender).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.result = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
                ViewBag.result = "failed";
            }
            if (ViewBag.result == "succses")
            {
                return PartialView("_ThanksSign");

            }
            else
            {
                return View(obj);
            }
        }


        public ActionResult statusChange(int user)
        {

            Guid activationCode = Guid.NewGuid();
            vender vender = db.venders.Where(a => a.venderid == user).FirstOrDefault();
            if (vender != null)
            {

                vender.Activationid = activationCode;
                db.Entry(vender).State = EntityState.Modified;

            }

            db.SaveChanges();
            //activation mail
            string fromemail = ConfigurationManager.AppSettings["fromemail"];
            string frompass = ConfigurationManager.AppSettings["frompass"];
            string emailhost = ConfigurationManager.AppSettings["emailhost"];
            int emailport = Convert.ToInt32(ConfigurationManager.AppSettings["emailport"]);
            string emailssl = ConfigurationManager.AppSettings["emailssl"];

            MailMessage mail = new MailMessage();
            mail.To.Add(vender.emailid);
            mail.From = new MailAddress(fromemail);
            mail.Subject = "Activation Mail";
            //string Body = "Hello, " + vender.name + "\n Your Account is activated please click  the  link  to login \n http://localhost:56348/login ";
            string Body = "Hello " + vender.name + ",";
            Body += "<br /><br />Please click the following link to activate your account";
            Body += "<br /><a href = '" + string.Format("{0}://{1}/Vender/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
            Body += "<br /><br />Thanks";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = emailhost;// "smtp.gmail.com";
            smtp.Port = emailport;// 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromemail, frompass);
            // ("marlusybanch@gmail.com", "yogita@09");// Enter seders User name and password
            if (emailssl == "1")
                smtp.EnableSsl = true;
            smtp.Send(mail);
            //end activation mail

            var objvenderuser = new List<Vender>();
            objvenderuser = CallVenderList();
            return View("AddVender", objvenderuser);

        }

        [HttpGet]
        public ActionResult VenderLogin()
        {
            VenderLogin objVenderLogin = new VenderLogin();
            return View(objVenderLogin);
        }

        [HttpPost]
        public ActionResult VenderLogin(VenderLogin objVenderLogin)
        {

            List<SelectListItem> listItemsBussiness = new List<SelectListItem>();
            listItemsBussiness.Add(new SelectListItem { Text = "--Select bussiness type--", Value = "0" });
            var cat = (from c in db.bussinesstypes select c).ToArray();
            for (int i = 0; i < cat.Length; i++)
            {
                listItemsBussiness.Add(new SelectListItem
                {
                    Text = cat[i].bussinesstypetext,
                    Value = cat[i].Id.ToString(),
                    Selected = (cat[i].Id == 0)
                });
            }
            ViewData["listItemsBussiness"] = listItemsBussiness;



            List<SelectListItem> listLanguage = new List<SelectListItem>();
            listLanguage.Add(new SelectListItem { Text = "--Select language--", Value = "0" });
            var listLangcat = (from c in db.Languages select c).ToArray();
            for (int i = 0; i < listLangcat.Length; i++)
            {
                listLanguage.Add(new SelectListItem
                {
                    Text = listLangcat[i].LanguageText,
                    Value = listLangcat[i].Id.ToString(),
                    Selected = (listLangcat[i].Id == 0)
                });
            }
            ViewData["listItemsLanguages"] = listLanguage;




            List<SelectListItem> listProvince = new List<SelectListItem>();
            listProvince.Add(new SelectListItem { Text = "--Select Province--", Value = "0" });
            var provinceCat = (from c in db.Provinces select c).ToArray();
            for (int i = 0; i < provinceCat.Length; i++)
            {
                listProvince.Add(new SelectListItem
                {
                    Text = provinceCat[i].ProvinceText,
                    Value = provinceCat[i].Id.ToString(),
                    Selected = (provinceCat[i].Id == 0)
                });
            }
            ViewData["listProvince"] = listProvince;


            List<SelectListItem> listBussiness = new List<SelectListItem>();
            listBussiness.Add(new SelectListItem { Text = "--Select time--", Value = "0" });
            var Bussiness = (from c in db.bussinessofficehours select c).ToArray();
            for (int i = 0; i < Bussiness.Length; i++)
            {
                listBussiness.Add(new SelectListItem
                {
                    Text = Bussiness[i].bussinessoffice,
                    Value = Bussiness[i].Id.ToString(),
                    Selected = (Bussiness[i].Id == 0)
                });
            }
            ViewData["listBussiness"] = listBussiness;

            ////
            bool? accepted = null;
            vender vender = null;
            VenderDetails objVenderDetails = new VenderDetails();
            teammanagement team = new teammanagement();
            try
            {
                if (ModelState.IsValid)
                {
                    vender = db.venders.Where(a => a.username == objVenderLogin.username && a.password == objVenderLogin.password && a.status == "Active").FirstOrDefault();
                    if (vender != null)
                    {
                        objVenderDetails.venderid = vender.venderid;
                        objVenderDetails.emailid = vender.emailid ?? "";
                        objVenderDetails.name = vender.name ?? "";
                        objVenderDetails.mobile_no = vender.mobile_no ?? "";
                        objVenderDetails.password = vender.password ?? "";
                        objVenderDetails.status = vender.status;
                        objVenderDetails.username = vender.username ?? "";
                        //objVenderDetails.image = vender.image ?? null;
                        FormsAuthentication.SetAuthCookie(vender.emailid, false);
                        Session["venderUserID"] = vender.venderid;
                        Session["venderName"] = vender.name ?? "";
                        Session["venderEmail"] = vender.emailid ?? "";
                        Session["venderUserName"] = vender.username ?? "";
                        // Session["venderImage"] = vender.image ?? "";
                        Session["IsVenderAdmin"] = true;
                        Session["VenderMemberID"] = 0;
                        Session["RoleID"] = 0;
                        ViewBag.VenderLogin = "succses";
                        accepted = vender.Accepted;
                    }
                    else
                    {

                        team = db.teammanagements.Where(t => t.email == objVenderLogin.username && t.password == objVenderLogin.password && t.status == true).FirstOrDefault();
                        if (team != null)
                        {
                            FormsAuthentication.SetAuthCookie(team.email, false);
                            Session["venderUserID"] = team.vender_id;
                            Session["venderName"] = team.membername ?? "";
                            Session["venderEmail"] = team.email ?? "";
                            Session["venderUserName"] = team.email ?? "";
                            Session["VenderMemberID"] = team.memberid;
                            Session["RoleID"] = team.roleid;
                            Session["IsVenderAdmin"] = false;
                            List<menuitem> roleMenu = new List<menuitem>();

                            ViewBag.venderMember = "succses";

                        }
                        else
                        {
                            ViewBag.VenderLogin = "Incorrect";
                            return View(objVenderLogin);
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
                ViewBag.VenderLogin = "failed";
            }
            if (ViewBag.VenderLogin == "succses" && accepted == false && Session["IsVenderAdmin"].ToString() == "true")
            {
                storedetail store = db.storedetails.Where(a => a.venderid == objVenderDetails.venderid).FirstOrDefault();
                if (store != null)
                {
                    //data storing if already login previously
                    // storedetail objstoredetail = new storedetail();
                    //  objVenderDetails.venderid = id;
                    objVenderDetails.business_type = store.business_type;
                    objVenderDetails.language = store.language;
                    objVenderDetails.display_name = store.display_name;
                    objVenderDetails.reg_business_name = store.reg_business_name;
                    objVenderDetails.business_address1 = store.business_address1;
                    objVenderDetails.business_address2 = store.business_address2;
                    objVenderDetails.Registration_Name = store.RegistrationName;
                    objVenderDetails.StoreProvince = store.Province;
                    objVenderDetails.city = store.city;
                    objVenderDetails.Bussiness_office_hours = store.Bussinessofficehours;
                    objVenderDetails.warehouse_address1 = store.warehouse_address1;
                    objVenderDetails.warehouse_address2 = store.warehouse_address2;
                    objVenderDetails.WarehouseCity = store.WarehouseCity;
                    objVenderDetails.WarehouseProvince = store.WarehouseProvince;
                    //  db.Entry(store).State = EntityState.Modified;
                    // db.SaveChanges();

                }
                return View("venderIndex", objVenderDetails);

            }
            else if (ViewBag.VenderLogin == "succses" && accepted == true && Session["IsVenderAdmin"].ToString() == "True")
            {
                return RedirectToAction("VenderRedirectIndex", "Vender", new { user = vender.venderid });

            }
            else if (ViewBag.venderMember == "succses" && Session["IsVenderAdmin"].ToString() == "False")
            {
                return RedirectToAction("VenderRedirectIndex", "Vender", new { user = team.vender_id });
            }
            else
            {
                //ViewBag.VenderLogin = "failed";
                return View(objVenderLogin);
            }
        }

        public static string GeneratePasswordByUserAndIteration(int userId, long iterationNumber, int digits = 6)
        {
            //Here the system converts the iteration number to a byte[]
            byte[] iterationNumberByte = BitConverter.GetBytes(iterationNumber);
            //To BigEndian (MSB LSB)
            if (BitConverter.IsLittleEndian) Array.Reverse(iterationNumberByte);

            //Hash the userId by HMAC-SHA-1 (Hashed Message Authentication Code)
            byte[] userIdByte = Encoding.ASCII.GetBytes(Convert.ToString(userId));
            HMACSHA1 userIdHMAC = new HMACSHA1(userIdByte, true);
            byte[] hash = userIdHMAC.ComputeHash(iterationNumberByte); //Hashing a message with a secret key

            //RFC4226 http://tools.ietf.org/html/rfc4226#section-5.4
            int offset = hash[hash.Length - 1] & 0xf; //0xf = 15d
            int binary =
                ((hash[offset] & 0x7f) << 24)      //0x7f = 127d
                | ((hash[offset + 1] & 0xff) << 16) //0xff = 255d
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);

            int password = binary % (int)Math.Pow(10, digits); // Shrink: 6 digits
            return password.ToString(new string('0', digits));
        }

        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string GetPassword(int userId)
        {
            // We get the "Unix Time" by sub from 1/1/1970 00:00:00 and now
            long iteration = (long)(DateTime.UtcNow - UNIX_EPOCH).TotalSeconds / 30;
            string generatedpassword = GeneratePasswordByUserAndIteration(userId, iteration);
            return generatedpassword;
        }

        public JsonResult GetOTP(int user)
        {
            int data = 0;
            if (user.ToString() != "" && user.ToString() != null)
            {
                int otpass;
                bool conversion = Int32.TryParse(GetPassword(user), out otpass);
                if (conversion)
                {
                    //Save user credentials.
                    vender vender = db.venders.Where(a => a.venderid == user).FirstOrDefault();
                    if (vender != null)
                    {
                        vender.verifyotp = otpass;
                        db.Entry(vender).State = EntityState.Modified;

                    }

                    db.SaveChanges();
                    // sendmsg();
                    //VenderDetails VenderOtp = new VenderDetails();
                    //VenderOtp.venderid = user;
                    //VenderOtp.Otp = otpass;
                    //VenderOtp.OtpCrtDate = DateTime.Now;
                    data = otpass;
                    ViewData["user"] = user;
                    ViewData["status"] = "OTP: " + otpass + " remains active just 30 seconds from now.";
                    return Json(data, JsonRequestBehavior.AllowGet);  //return View("venderIndex", VenderOtp);
                }
                else
                {
                    ViewData["status"] = "Sorry, an error was found while creating your password. Try again, please.";
                    return Json(data, JsonRequestBehavior.AllowGet); // return View("UserGetOtp");
                }
            }
            else
                return Json(data, JsonRequestBehavior.AllowGet); // return View("venderIndex");
        }



        public ActionResult Activation()
        {
            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());
                vender vender = db.venders.Where(a => a.Activationid == activationCode).FirstOrDefault();
                if (vender != null)
                {
                    vender.Activationid = Guid.Empty;
                    vender.status = "Active";
                    db.Entry(vender).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "Activation successful.";

                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult venderIndex(VenderDetails objVenderDetails)
        {
            // var id = objVenderDetails.venderid;
            storedetail store = db.storedetails.Where(a => a.venderid == objVenderDetails.venderid).FirstOrDefault();
            if (store != null)
            {
                //data storing if already login previously
                // storedetail objstoredetail = new storedetail();
                //  objVenderDetails.venderid = id;
                objVenderDetails.business_type = store.business_type;
                objVenderDetails.language = store.language;
                objVenderDetails.display_name = store.display_name;
                objVenderDetails.reg_business_name = store.reg_business_name;
                objVenderDetails.business_address1 = store.business_address1;
                objVenderDetails.business_address2 = store.business_address2;
                objVenderDetails.Registration_Name = store.RegistrationName;
                objVenderDetails.StoreProvince = store.Province;
                objVenderDetails.city = store.city;
                objVenderDetails.Bussiness_office_hours = store.Bussinessofficehours;
                objVenderDetails.warehouse_address1 = store.warehouse_address1;
                objVenderDetails.warehouse_address2 = store.warehouse_address2;
                objVenderDetails.WarehouseCity = store.WarehouseCity;
                objVenderDetails.WarehouseProvince = store.WarehouseProvince;
                //db.Entry(store).State = EntityState.Modified;
                //db.SaveChanges();

            }


            //
            //bussinesslistitem
            List<SelectListItem> listItemsBussiness = new List<SelectListItem>();
            listItemsBussiness.Add(new SelectListItem { Text = "--Select bussiness type--", Value = "0" });
            var cat = (from c in db.bussinesstypes select c).ToArray();
            for (int i = 0; i < cat.Length; i++)
            {
                listItemsBussiness.Add(new SelectListItem
                {
                    Text = cat[i].bussinesstypetext,
                    Value = cat[i].Id.ToString(),
                    Selected = (cat[i].Id == 0)
                });
            }
            ViewData["listItemsBussiness"] = listItemsBussiness;
            //

            //bussinesslistitem
            List<SelectListItem> listLanguage = new List<SelectListItem>();
            listLanguage.Add(new SelectListItem { Text = "--Select language--", Value = "0" });
            var listLangcat = (from c in db.Languages select c).ToArray();
            for (int i = 0; i < listLangcat.Length; i++)
            {
                listLanguage.Add(new SelectListItem
                {
                    Text = listLangcat[i].LanguageText,
                    Value = listLangcat[i].Id.ToString(),
                    Selected = (listLangcat[i].Id == 0)
                });
            }
            ViewData["listItemsLanguages"] = listLanguage;
            //

            //Select province

            List<SelectListItem> listProvince = new List<SelectListItem>();
            listProvince.Add(new SelectListItem { Text = "--Select Province--", Value = "0" });
            var provinceCat = (from c in db.Provinces select c).ToArray();
            for (int i = 0; i < provinceCat.Length; i++)
            {
                listProvince.Add(new SelectListItem
                {
                    Text = provinceCat[i].ProvinceText,
                    Value = provinceCat[i].Id.ToString(),
                    Selected = (provinceCat[i].Id == 0)
                });
            }
            ViewData["listProvince"] = listProvince;
            //
            //Bussiness Hours Time
            List<SelectListItem> listBussiness = new List<SelectListItem>();
            listBussiness.Add(new SelectListItem { Text = "--Select time--", Value = "0" });
            var Bussiness = (from c in db.bussinessofficehours select c).ToArray();
            for (int i = 0; i < Bussiness.Length; i++)
            {
                listBussiness.Add(new SelectListItem
                {
                    Text = Bussiness[i].bussinessoffice,
                    Value = Bussiness[i].Id.ToString(),
                    Selected = (Bussiness[i].Id == 0)
                });
            }
            ViewData["listBussiness"] = listBussiness;
            //


            return View(objVenderDetails);
        }



        public JsonResult StepVerify(int Value)
        {
            var data = "";
            vender vender = db.venders.Where(a => a.verifyotp == Value).FirstOrDefault();
            if (vender != null)
            {

                vender.verifyotp = 1234567890;
                db.Entry(vender).State = EntityState.Modified;
                db.SaveChanges();
                data = "true";

            }

            else
            {
                data = "false";
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StepContinue(int id, string Busnesstype, string Lang, string Disp_Name, string RegiBusnName, string RegisBusnAd, string RegisBusnAd2, string RegisName, string Prov, string City, string Busnoffhours, string Warehousead1, string Warehousead2, string WarehouseCity, string WarehouseProvince)
        {
            var data = "";
            try
            {
                /////
                storedetail objstoredetail = new storedetail();
                storedetail store = db.storedetails.Where(a => a.venderid == id).FirstOrDefault();
                if (store != null)
                {
                    //data storing if already login previously
                    // storedetail objstoredetail = new storedetail();
                    //  objVenderDetails.venderid = id;
                    store.business_type = Busnesstype;
                    store.language = Lang;
                    store.display_name = Disp_Name;
                    store.reg_business_name = RegiBusnName;
                    store.business_address1 = RegisBusnAd;
                    store.business_address2 = RegisBusnAd2;
                    store.RegistrationName = RegisName;
                    store.Province = Prov;
                    store.city = City;
                    store.Bussinessofficehours = Busnoffhours;
                    store.warehouse_address1 = Warehousead1;
                    store.warehouse_address2 = Warehousead2;
                    store.WarehouseCity = WarehouseCity;
                    store.WarehouseProvince = WarehouseProvince;
                    db.Entry(store).State = EntityState.Modified;
                    db.SaveChanges();
                    data = "true";

                }
                else
                {
                    /////

                    objstoredetail.venderid = id;
                    objstoredetail.business_type = Busnesstype;
                    objstoredetail.language = Lang;
                    objstoredetail.display_name = Disp_Name;
                    objstoredetail.reg_business_name = RegiBusnName;
                    objstoredetail.business_address1 = RegisBusnAd;
                    objstoredetail.business_address2 = RegisBusnAd2;
                    objstoredetail.RegistrationName = RegisName;
                    objstoredetail.Province = Prov;
                    objstoredetail.city = City;
                    objstoredetail.Bussinessofficehours = Busnoffhours;
                    objstoredetail.warehouse_address1 = Warehousead1;
                    objstoredetail.warehouse_address2 = Warehousead2;
                    objstoredetail.WarehouseCity = WarehouseCity;
                    objstoredetail.WarehouseProvince = WarehouseProvince;
                    //objstoredetail.business_address1 = data;       
                    db.storedetails.Add(objstoredetail);
                    db.SaveChanges();
                    data = "true";
                }
            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
                data = "false";
            }


            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Accept(int user)
        {
            var data = "";
            vender vender = db.venders.Where(a => a.venderid == user).FirstOrDefault();
            if (vender != null)
            {

                vender.Accepted = true;
                db.Entry(vender).State = EntityState.Modified;
                db.SaveChanges();
                data = "true";

            }

            else
            {
                data = "false";
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserAgreement(int User)
        {

            //dropdown start
            //ViewBag.Companytype = new SelectList(db.CompanyTypes.ToList(), "Id", "CompanyType");



            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "-- Select Company --", Value = "0" });
            var cat = (from c in db.CompanyTypes select c).ToArray();
            for (int i = 0; i < cat.Length; i++)
            {
                list.Add(new SelectListItem
                {
                    Text = cat[i].CompanyTypeText,
                    Value = cat[i].Id.ToString(),
                    Selected = (cat[i].Id == 0)
                });
            }
            ViewData["listItems"] = list;

            //listItemsbank
            List<SelectListItem> listItemsbank = new List<SelectListItem>();
            listItemsbank.Add(new SelectListItem { Text = "-- Select Option --", Value = "0" });
            var objList = (from c in db.banks select c).ToArray();
            for (int i = 0; i < objList.Length; i++)
            {
                listItemsbank.Add(new SelectListItem
                {
                    Text = objList[i].BankName,
                    Value = objList[i].Id.ToString(),
                    Selected = (objList[i].Id == 0)
                });
            }
            ViewData["listItemsbank"] = listItemsbank;


            //dropdown end
            VenderAddProff objVenderAddProff = new VenderAddProff();
            List<Agreement> objAgreementDetails = new List<Agreement>();
            //  var objagreement = db.agreements.Where(a => a.VenderId == User).ToList();
            ////
            var objagreementvalue = (from d in db.agreements
                                     join dr in db.filestores on d.FileId equals dr.FileId
                                     where d.VenderId == User
                                     select new
                                     {
                                         FileId = d.FileId,
                                         AgreementVersion = d.AgreementVersion,
                                         AcceptedDate = d.AcceptedDate,
                                         fileName = dr.fileName,
                                         fileSize = dr.fileSize,
                                         FileExtension = dr.FileExtension,
                                         attachment = dr.attachment
                                     }).ToList();

            // objVenderAddProff.Agreementlist = objagreement;
            foreach (var a in objagreementvalue)
            {
                objAgreementDetails.Add(new Agreement
                {
                    FileId = a.FileId,
                    AgreementVersion = a.AgreementVersion,
                    AcceptedDate = a.AcceptedDate,
                    attachment = a.attachment,
                    fileName = a.fileName,
                    fileSize = a.fileSize
                });

            }

            objVenderAddProff.Agreementlist = objAgreementDetails;
            //venderaddressproof objDBAddProff = new venderaddressproof();
            //venderbankdetail objbankdetail = new venderbankdetail();
            var dbVender = db.venders.Where(a => a.venderid == User).FirstOrDefault();
            var dbAddProff = db.venderaddressproofs.Where(a => a.venderid == User).FirstOrDefault();
            var dbBankDetails = db.venderbankdetails.Where(a => a.venderid == User).FirstOrDefault();
            objVenderAddProff.venderid = User;

            if (dbVender != null)
            {
                objVenderAddProff.emailid = dbVender.emailid;
                objVenderAddProff.name = dbVender.name;
                objVenderAddProff.image = dbVender.image.ToString();
                //objVenderAddProff.address_image = dbVender.emailid;
            }


            if (dbAddProff != null)
            {

                objVenderAddProff.CompanyTradeName = dbAddProff.CompanyTradeName;
                objVenderAddProff.CompanyType = dbAddProff.CompanyType;
                objVenderAddProff.Nationality = dbAddProff.CompanyTradeName;
                objVenderAddProff.MainOffice = dbAddProff.CompanyType;
                objVenderAddProff.POBox = dbAddProff.CompanyTradeName;
                objVenderAddProff.PostalCode = dbAddProff.CompanyType;
                objVenderAddProff.TlelphoneNumber = dbAddProff.CompanyTradeName;
                objVenderAddProff.RegistrationDate = dbAddProff.CompanyType;
                //objVenderAddProff.CompanyType2 = dbAddProff.CompanyType2;
            }

            if (dbBankDetails != null)
            {
                objVenderAddProff.bank_name = dbBankDetails.bank_name;
                objVenderAddProff.HolderName = dbBankDetails.HolderName;
                objVenderAddProff.IBANCode = dbBankDetails.IBANCode;
            }

            // var listItems = new List<ListItem> { new ListItem { Text = "Slect company ", Value = "0" }, new ListItem { Text = "company A", Value = "1" }, new ListItem { Text = "company B", Value = "2//" } };

            return View(objVenderAddProff);
        }

        public FileResult content(int FileValue)
        {
            // var wordDocumentFilePath = "/Content/images/instapay-links.docx";
            //  byte[] bytes = System.IO.File.ReadAllBytes(wordDocumentFilePath);
            var contentValue = db.filestores.Where(a => a.FileId == FileValue).FirstOrDefault();

            byte[] fileBytes = contentValue.attachment;
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, contentValue.fileName);
            //fileBytes.ge
        }



        public JsonResult VerifyEmail(string user)
        {
            var data = "false";
            List<vender> vender = db.venders.Where(a => a.emailid != null && a.emailid != "").ToList();
            if (vender != null)
            {

                foreach (var A in vender)
                {
                    if (A.emailid == user)
                    {
                        data = "true";
                        break;
                    }

                }


            }

            else
            {
                data = "false";
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserAgreementSubmit(int user, string Bank, string Holder, string IBAN, string TradeName, string ComType, string Nationality, string MainOffice, string POBox, string Postal, string TphNum, string RegisDate)
        {
            var data = "false";
            venderaddressproof objVenAddProff = new venderaddressproof();
            venderbankdetail objVenBankDetail = new venderbankdetail();

            try
            {

                //  vender = db.venders.Where(a => a.username == objVenderLogin.username && a.password == objVenderLogin.password && a.status == true).FirstOrDefault();
                //if (vender != null)
                var dbAddProff = db.venderaddressproofs.Where(a => a.venderid == user).FirstOrDefault();
                var dbBankDetails = db.venderbankdetails.Where(a => a.venderid == user).FirstOrDefault();

                if (dbAddProff != null)
                {
                    dbAddProff.Nationality = Nationality;
                    dbAddProff.POBox = POBox;
                    dbAddProff.PostalCode = Postal;
                    dbAddProff.RegistrationDate = RegisDate;
                    dbAddProff.TlelphoneNumber = TphNum;
                    dbAddProff.CompanyTradeName = TradeName;
                    dbAddProff.CompanyType = ComType;
                    //  dbAddProff.CompanyType2 = ComType2;
                    dbAddProff.MainOffice = MainOffice;
                    db.Entry(dbAddProff).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    objVenAddProff.venderid = user;
                    objVenAddProff.Nationality = Nationality;
                    objVenAddProff.POBox = POBox;
                    objVenAddProff.PostalCode = Postal;
                    objVenAddProff.RegistrationDate = RegisDate;
                    objVenAddProff.TlelphoneNumber = TphNum;
                    objVenAddProff.CompanyTradeName = TradeName;
                    objVenAddProff.CompanyType = ComType;
                    //  objVenAddProff.CompanyType2 = ComType2;
                    objVenAddProff.MainOffice = MainOffice;
                    db.venderaddressproofs.Add(objVenAddProff);

                    db.SaveChanges();
                }

                if (dbBankDetails != null)
                {

                    dbBankDetails.bank_name = Bank;
                    dbBankDetails.HolderName = Holder;
                    dbBankDetails.IBANCode = IBAN;
                    db.Entry(dbBankDetails).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                    objVenBankDetail.bank_name = Bank;
                    objVenBankDetail.HolderName = Holder;
                    objVenBankDetail.IBANCode = IBAN;
                    objVenBankDetail.venderid = user;
                    db.venderbankdetails.Add(objVenBankDetail);
                    db.SaveChanges();
                }
                // objVenAddProff.status = vender.status;
                //  objVenAddProff.username = vender.username;
                data = "true";


            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
                data = "false";
            }

            // return View("venderIndex", objVenderDetails);
            // return View(objVenderAddProff);
            return Json(data, JsonRequestBehavior.AllowGet);



            //  return View(objVenderAddProff);
        }


        public ActionResult VenderRedirectIndex(int user)
        {
            VenderDetails objVenderDetails = new VenderDetails();
            var dbVender = db.venders.Where(a => a.venderid == user).FirstOrDefault();
            if (dbVender != null)
            {
                objVenderDetails.name = dbVender.name ?? "";
                objVenderDetails.emailid = dbVender.emailid ?? "";
                objVenderDetails.username = dbVender.username ?? "";
                objVenderDetails.venderid = dbVender.venderid;
                objVenderDetails.image = null;
                int roleID = Convert.ToInt16(Session["RoleID"]);
                var roleMenuI = (from rm in db.rolemanagements
                                 join m in db.menuitems on rm.menuid equals m.menuid
                                 where rm.roleid == roleID
                                 select new
                                 {
                                     m.menuname,
                                     rm.menuid
                                 }).ToList()
                                        .Select(x => new MenuItem()
                                        {
                                            menuid = (int)x.menuid,
                                            menuname = x.menuname
                                        }).AsEnumerable();

                Session["menuPermission"] = roleMenuI;
                Session["roleID"] = roleID;
                Session["name"] = dbVender.name ?? "";
                Session["emailid"] = dbVender.emailid ?? "";
                Session["username"] = dbVender.username ?? "";
                Session["venderid"] = dbVender.venderid;
                Session["profileImage"] = null;
                //ViewBag.menuPermission = roleMenuI;
                //ViewBag.roleID = roleID;
                //ViewBag.name = dbVender.name ?? "";
                //ViewBag.emailid = dbVender.emailid ?? "";
                //ViewBag.username = dbVender.username ?? "";
                //ViewBag.venderid = dbVender.venderid;
                //ViewBag.profileImage = null;
            }


            return View(objVenderDetails);
        }

        [ChildActionOnly]
        public ActionResult VenderProfile(int user)
        {
            VenderAddProff objVenderAddProff = new VenderAddProff();
            vender objvender = db.venders.FirstOrDefault(a => a.venderid == user);
            venderaddressproof objvenderaddressproof = db.venderaddressproofs.FirstOrDefault(a => a.venderid == user);
            venderbankdetail objvenderbankdetail = db.venderbankdetails.FirstOrDefault(a => a.venderid == user);
            storedetail objstoredetail = db.storedetails.FirstOrDefault(a => a.venderid == user);
            objVenderAddProff.venderid = user;
            if (objvender != null)
            {
                objVenderAddProff.MerchantId = objvender.MerchantId;
                objVenderAddProff.name = objvender.name;

                objVenderAddProff.emailid = objvender.emailid;
                objVenderAddProff.mobile_no = objvender.mobile_no;
                objVenderAddProff.AdditionalInfor = objvender.AdditionalInfor;
            }

            if (objvenderbankdetail != null)
            {
                objVenderAddProff.VatIdentityFicationNumber = objvenderbankdetail.VatIdentityFicationNumber;
                objVenderAddProff.benificary_name_in_bank = objvenderbankdetail.benificary_name_in_bank;
                objVenderAddProff.account_no = objvenderbankdetail.account_no;

                objVenderAddProff.BankNameOther = objvenderbankdetail.BankNameCaseOther ?? "";


                objVenderAddProff.branch_name = objvenderbankdetail.branch_name;
                objVenderAddProff.ifsc_code = objvenderbankdetail.ifsc_code;
            }
            //else
            //{
            //    objVenderAddProff.VatIdentityFicationNumber = "";
            //    objVenderAddProff.benificary_name_in_bank = "";
            //    objVenderAddProff.account_no = 00000000000000;
            //    objVenderAddProff.bank_name = objvenderbankdetail.bank_name;
            //    objVenderAddProff.branch_name = objvenderbankdetail.branch_name;
            //    objVenderAddProff.ifsc_code = objvenderbankdetail.ifsc_code;

            //}
            if (objvenderaddressproof != null)
            {
                objVenderAddProff.Address = objvenderaddressproof.Address;
                objVenderAddProff.City = objvenderaddressproof.City;
                objVenderAddProff.State = objvenderaddressproof.State;
                objVenderAddProff.PanNumber = objvenderaddressproof.PanNumber;
            }

            if (objstoredetail != null)
            {
                objVenderAddProff.display_name = objstoredetail.display_name;
            }

            ViewBag.valueoftab = "profile";

            return PartialView("ProfileTab", objVenderAddProff);
        }


        public JsonResult VenderProfileSubmit(int user, string MerchantId, string name, string display_name, string emailid, string mobile_no, string AdditionalInfor, string PanNumber, string VatIdentity, string benificary_name, string account_no, string bank_name, string branch_name, string ifsc_code, string Address, string City, string State)
        {
            var data = "false";
            venderaddressproof objAddProffNoExist = new venderaddressproof();
            venderbankdetail objBankDetalNoExist = new venderbankdetail();

            try
            {

                //    VenderAddProff objVenderAddProff = new VenderAddProff();
                vender objvender = db.venders.FirstOrDefault(a => a.venderid == user);
                venderaddressproof objvenderaddressproof = db.venderaddressproofs.FirstOrDefault(a => a.venderid == user);
                venderbankdetail objvenderbankdetail = db.venderbankdetails.FirstOrDefault(a => a.venderid == user);
                storedetail objstoredetail = db.storedetails.FirstOrDefault(a => a.venderid == user);
                if (objvender != null)
                {
                    //objvender.venderid = user;
                    objvender.MerchantId = MerchantId;
                    objvender.name = name;

                    objvender.emailid = emailid;
                    objvender.mobile_no = mobile_no;
                    objvender.AdditionalInfor = AdditionalInfor;
                    db.Entry(objvender).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (objvenderbankdetail != null)
                {

                    objvenderbankdetail.VatIdentityFicationNumber = VatIdentity;
                    objvenderbankdetail.benificary_name_in_bank = benificary_name;
                    objvenderbankdetail.account_no = Convert.ToInt32(account_no);
                    // objvenderbankdetail.bank_name = bank_name;

                    objvenderbankdetail.BankNameCaseOther = bank_name ?? "";

                    objvenderbankdetail.branch_name = branch_name;
                    objvenderbankdetail.ifsc_code = ifsc_code;
                    db.Entry(objvenderbankdetail).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    objBankDetalNoExist.VatIdentityFicationNumber = VatIdentity;
                    objBankDetalNoExist.benificary_name_in_bank = benificary_name;
                    objBankDetalNoExist.account_no = Convert.ToInt32(account_no);
                    objBankDetalNoExist.BankNameCaseOther = bank_name ?? "";
                    // objBankDetalNoExist.bank_name= "9";

                    objBankDetalNoExist.branch_name = branch_name;
                    objBankDetalNoExist.ifsc_code = ifsc_code;
                    objBankDetalNoExist.venderid = user;
                    db.venderbankdetails.Add(objBankDetalNoExist);
                    db.SaveChanges();
                }

                if (objvenderaddressproof != null)
                {

                    objvenderaddressproof.Address = Address;
                    objvenderaddressproof.City = City;
                    objvenderaddressproof.State = State;
                    objvenderaddressproof.PanNumber = PanNumber;
                    db.Entry(objvenderaddressproof).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    objAddProffNoExist.Address = Address;
                    objAddProffNoExist.City = City;
                    objAddProffNoExist.State = State;
                    objAddProffNoExist.PanNumber = PanNumber;
                    objAddProffNoExist.venderid = user;
                    db.venderaddressproofs.Add(objAddProffNoExist);
                    db.SaveChanges();

                }

                if (objstoredetail != null)
                {
                    objstoredetail.display_name = display_name;
                    db.Entry(objstoredetail).State = EntityState.Modified;
                    db.SaveChanges();
                }

                data = "true";
                //  ViewBag.tab = "profile";


            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
                data = "false";
            }

            // return View("venderIndex", objVenderDetails);
            // return View(objVenderAddProff);
            return Json(data, JsonRequestBehavior.AllowGet);



            //  return View(objVenderAddProff);
        }
        //public ActionResult VenderProfile(VenderAddProff objProfile)
        //{


        //    return View(objProfile);
        //}


        public ActionResult VenderLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("VenderLogin", "Vender");
        }




        #region Product Managemement 
        /*Start Vender Product Management*/

        public class SelectListViewModel
        {
            public int KeyID { get; set; }
            public string Value { get; set; }
            public int Selected { get; set; }
        }

        private List<SelectListItem> setDropDownVender()
        {
            using (ThibanWaterDBEntities DB = new ThibanWaterDBEntities())
            {
                List<SelectListItem> returnList = new List<SelectListItem>();

                //returnList = (selectList as List<SelectedItemViewModel>).Select(x =>
                //   new SelectListItem
                //   {
                //       Value = Convert.ToString(x.Value),
                //       Text = Convert.ToString(x.Text),
                //       Selected = x.Selected
                //   }).ToList();
                //var strItem = item.Key.ToString();


                returnList = DB.venders.Where(x => string.IsNullOrEmpty(x.name) == false).Select(x =>
                            new SelectListItem
                            {
                                Value = x.venderid.ToString(),
                                Text = x.name,
                                Selected = false
                            }).ToList();

                ViewBag.VenderList = returnList;
                return returnList;
            }
        }

        private List<Product> CallProductList()
        {
            using (ThibanWaterDBEntities DB = new ThibanWaterDBEntities())
            {
                List<Product> objProduct = new List<Product>();
                int vederProdID = Convert.ToInt16(Session["venderUserID"]);
                var productQuery = (from prod in DB.products
                                    where prod.status != "Deleted"
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid
                                    where ve.venderid == vederProdID
                                    select new
                                    {
                                        prod.productid,
                                        prod.vender_id,
                                        ve.name,
                                        prod.description,
                                        prod.product_title,
                                        prod.customer_price,
                                        prod.customer_min_order_quantity,
                                        prod.customer_max_order_quantity,
                                        prod.store_price,
                                        prod.store_min_order_quantity,
                                        prod.store_max_order_quantity,
                                        prod.phno,
                                        prod.bottle_per_box,
                                        prod.stock,
                                        prod.sku,
                                        prod.status,
                                        ca.category_name,
                                        prod.category_id,
                                        prod.ProductAvaibility,
                                        prod.volume,
                                        prod.bottle_material,
                                        prod.brand,
                                        prod.av_composition_ppm,
                                        prod.discount,
                                        prod.Image_path
                                    }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product()
                    {

                        availabilityName = item.ProductAvaibility,
                        av_composition_ppm = item.av_composition_ppm,
                        bottle_per_box = item.bottle_per_box,
                        brand = item.brand,
                        categoryname = item.category_name,
                        category_id = item.category_id,
                        customer_max_order = item.customer_max_order_quantity,
                        customer_min_order = item.customer_min_order_quantity,
                        customer_price = item.customer_price,
                        Description = item.description,
                        discount = item.discount,
                        Image_path = item.Image_path,
                        phno = item.phno,
                        ProductId = item.productid,
                        ProductSKU = item.sku,
                        Status = item.status,
                        stock = item.stock,
                        store_max_order = item.store_max_order_quantity,
                        store_min_order = item.store_min_order_quantity,
                        store_price = item.store_price,
                        Title = item.product_title,
                        Vendername = item.name,
                        VenderSource = item.vender_id,
                        volume = item.volume,
                        bottle_material = item.bottle_material

                    });
                }
                //setDropDownVender();
                return objProduct;
            }
        }
        [HttpGet]
        public ActionResult VenderProductManagement(int? page, int? pageSizeValue, string filter, string filterStatus)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                //List<ProductRating> objProductRating = new List<ProductRating>();
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);

                var objProduct = new List<Product>();
                objProduct = CallProductList();
                //   var objFilterProduct = new List<Product>();
                if (filter == null || filter == "")
                {
                    ViewBag.filter_status = filterStatus;
                    ViewBag.filter_order = filterStatus;
                    ViewBag.filter_brand = filterStatus;
                }
                else if (filterStatus != null || filterStatus != "")
                {
                    //    objFilterProduct = CallProductList();
                    if (filter == "Status")
                    {

                        if (filterStatus == "All" || (filterStatus == "" || filterStatus == ""))
                        {
                            // objFilterProduct = objProduct.ToList();
                        }
                        else
                        {
                            objProduct = objProduct.Where(x => x.Status == filterStatus).ToList();
                        }
                        ViewBag.filter_status = filterStatus;
                    }
                    else if (filter == "OrderCount")
                    {
                        //Order Count Filter
                        if (filterStatus != "" && filterStatus != "0")
                        {

                            int startCount = Convert.ToInt16(filterStatus.Split('-')[0]);
                            int secondCount = Convert.ToInt16(filterStatus.Split('-')[1]);
                            if (secondCount != 0)
                            {
                                List<int?> ProductIDs = new List<int?>();
                                ProductIDs = db.orders.GroupBy(x => x.product_id).Where(grp => grp.Count() > startCount && grp.Count() < secondCount).Select(x => x.Key).ToList();
                                var obj = from a in objProduct
                                          join p in ProductIDs on a.ProductId equals (p.HasValue ? p.Value : 0)
                                          select a;
                                objProduct = obj.ToList();
                            }
                            else
                            {
                                List<int?> ProductIDs = new List<int?>();
                                ProductIDs = db.orders.GroupBy(x => x.product_id).Where(grp => grp.Count() > startCount).Select(x => x.Key).ToList();
                                var obj = from a in objProduct
                                          join p in ProductIDs on a.ProductId equals (p.HasValue ? p.Value : 0)
                                          select a;
                                objProduct = obj.ToList();
                            }
                        }
                        ViewBag.filter_order = filterStatus;
                    }
                    else if (filter == "Brand")
                    {
                        ViewBag.filter_brand = filterStatus;
                    }

                }


                return View("VenderProductManagement", objProduct.ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpPost]
        public ActionResult VenderProductManagement(Product p, HttpPostedFileBase file, int? page, int? pageSizeValue)
        {
            try
            {
                product prod = new product();
                using (ThibanWaterDBEntities DB = new ThibanWaterDBEntities())
                {

                    if (ModelState.IsValid)
                    {
                        ImageFile obj = new ImageFile();

                        if (Request.Files.Count > 0)
                        {
                            int id = 0;
                            int flagimg = 0;
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
                                //    prod.image = id;
                                //}
                                string targetFolder = Server.MapPath("~/content/vender/images_product/");
                                bool exists = System.IO.Directory.Exists(targetFolder);
                                if (!exists)
                                    System.IO.Directory.CreateDirectory(targetFolder);
                                string targetPath = Path.Combine(targetFolder, "prod_" + p.ProductId + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                                flagimg = 1;
                                file.SaveAs(targetPath);
                                prod.Image_path = "~/content/vender/images_product/prod_" + p.ProductId + file.FileName.Substring(file.FileName.LastIndexOf("."));
                            }
                            if (p.ProductId == 0)
                            {

                                prod.vender_id = p.VenderSource;
                                prod.description = p.Description;
                                prod.product_title = p.Title;
                                prod.customer_price = p.customer_price;
                                prod.customer_min_order_quantity = p.customer_min_order;
                                prod.customer_max_order_quantity = p.customer_max_order;
                                prod.store_price = p.store_price;
                                prod.store_min_order_quantity = p.store_min_order;
                                prod.store_max_order_quantity = p.store_max_order;
                                prod.phno = p.phno;
                                prod.bottle_per_box = p.bottle_per_box;
                                prod.stock = p.stock;
                                prod.sku = p.ProductSKU;
                                prod.status = p.Status;
                                prod.category_id = p.category_id;
                                prod.ProductAvaibility = p.availabilityName;
                                prod.volume = p.volume;
                                prod.bottle_material = p.bottle_material;
                                prod.brand = p.brand;
                                prod.av_composition_ppm = p.av_composition_ppm;
                                prod.discount = p.discount;
                                prod.bottle_per_box = p.bottle_per_box;
                                prod.createdby = Convert.ToInt32(Session["venderUserID"]);
                                DB.products.Add(prod);
                                DB.SaveChanges();

                                ViewBag.status = 1;
                                ViewBag.message = "Product Added Successfully.";
                            }
                            else if (p.ProductId > 0)
                            {
                                product pd = db.products.First(x => x.productid == p.ProductId);
                                pd.vender_id = p.VenderSource;
                                pd.description = p.Description;
                                pd.product_title = p.Title;
                                pd.product_title = p.Title;
                                pd.customer_price = p.customer_price;
                                pd.customer_min_order_quantity = p.customer_min_order;
                                pd.customer_max_order_quantity = p.customer_max_order;
                                pd.store_price = p.store_price;
                                pd.store_min_order_quantity = p.store_min_order;
                                pd.store_max_order_quantity = p.store_max_order;
                                pd.phno = p.phno;
                                pd.bottle_per_box = p.bottle_per_box;
                                pd.stock = p.stock;
                                pd.sku = p.ProductSKU;
                                pd.status = p.Status;
                                pd.category_id = p.category_id;
                                pd.ProductAvaibility = p.availabilityName;
                                pd.volume = p.volume;
                                pd.bottle_material = p.bottle_material;
                                pd.brand = p.brand;
                                pd.av_composition_ppm = p.av_composition_ppm;
                                pd.discount = p.discount;
                                prod.bottle_per_box = p.bottle_per_box;
                                prod.createdby = Convert.ToInt32(Session["venderUserID"]);

                                if (!string.IsNullOrEmpty(prod.Image_path) && flagimg == 1)
                                {
                                    pd.Image_path = prod.Image_path;
                                }
                                db.SaveChanges();
                                ViewBag.status = 1;
                                ViewBag.message = "Product Edited Successfully";
                            }

                        }
                        else
                        {
                            ViewBag.productretrun = p;
                            ViewBag.status = 0;
                            ModelState.AddModelError("", "Product Already Added.");
                        }
                    }
                    else
                    {
                        ViewBag.productretrun = p;
                        ViewBag.status = 0;
                    }
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

            var objProduct = new List<Product>();
            objProduct = CallProductList();
            /////////
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            return View("VenderProductManagement", objProduct.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult UpdateProductStatus(int prodid, string sts)
        {
            product prod = new product();
            db.products.Find(prodid).status = sts;
            var id = prodid;
            prod.status = sts;
            db.SaveChanges();
            return Json(new { productid = prodid, status = sts });
        }

        [HttpPost]
        public JsonResult DeleteProduct(string[] prodid)
        {
            JsonResult res = new JsonResult();
            //res = cu.Where(x => x.Status == status).ToList();
            if (prodid != null && prodid.Length > 0)
            {
                foreach (string i in prodid)
                {
                    int id = Convert.ToInt16(i);
                    db.products.Find(id).status = "Deleted";
                }
                db.SaveChanges();
            }
            return Json(true);

        }
        /*End*/
        #endregion

        #region Product Rating Management
        [HttpGet]
        public ActionResult VenderProductRating(int? page, int? pageSizeValue)//
        {
            int venderID = Convert.ToInt16(Session["venderID"]);
            List<ProductRating> objProductRating = new List<ProductRating>();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            try
            {
                // throw new Exception();
                var objListproducts = db.products.ToList();

                foreach (var listproducts in objListproducts)
                {

                    var objRatings = (from p in db.products
                                      join pr in db.productrattings on p.productid equals pr.product_id
                                      join vend in db.venders on p.vender_id equals vend.venderid
                                      join img in db.ImageFiles on vend.image equals img.ImageId
                                      where p.productid == listproducts.productid && pr.status != "Deleted" && p.vender_id == venderID
                                      select new
                                      {
                                          rateid = pr.rateid,
                                          ratting = pr.ratting,
                                          comment = pr.comment,
                                          productid = p.productid,
                                          product_title = p.product_title,
                                          venderid = vend.venderid,
                                          emailid = vend.emailid,
                                          username = vend.username,
                                          image = img.Imageattachment
                                      }).ToList();

                    foreach (var a in objRatings)
                    {

                        objProductRating.Add(new ProductRating
                        {
                            rateid = a.rateid,
                            ratting = a.ratting,
                            comment = a.comment,
                            productid = a.productid,
                            product_title = a.product_title,
                            venderid = a.venderid,
                            emailid = a.emailid,
                            username = a.username,
                            image = a.image
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
            }
            return View(objProductRating.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult VenderProductDetails(int Id)
        {
            ProductRating objProductRating = new ProductRating();
            var objRatings = (from p in db.products
                              join pr in db.productrattings on p.productid equals pr.product_id
                              join vend in db.venders on p.vender_id equals vend.venderid
                              join img in db.ImageFiles on vend.image equals img.ImageId
                              where pr.rateid == Id
                              select new
                              {
                                  rateid = pr.rateid,
                                  ratting = pr.ratting,
                                  comment = pr.comment,
                                  productid = p.productid,
                                  product_title = p.product_title,
                                  venderid = vend.venderid,
                                  emailid = vend.emailid,
                                  username = vend.username,
                                  image = img.Imageattachment
                              }).FirstOrDefault();
            if (objRatings != null)
            {
                objProductRating.rateid = objRatings.rateid;
                objProductRating.ratting = objRatings.ratting;
                objProductRating.comment = objRatings.comment;
                objProductRating.productid = objRatings.productid;
                objProductRating.product_title = objRatings.product_title;
                objProductRating.venderid = objRatings.venderid;
                objProductRating.emailid = objRatings.emailid;
                objProductRating.username = objRatings.username;
                objProductRating.image = objRatings.image;

            }
            return PartialView("_VenderProductDetails", objProductRating);
        }

        [HttpPost]
        public JsonResult DeleteProductRating(string[] ids)
        {
            JsonResult res = new JsonResult();
            if (ids != null && ids.Length > 0)
            {
                foreach (string i in ids)
                {
                    int id = Convert.ToInt16(i);
                    db.productrattings.Find(id).status = "Deleted";
                }
                db.SaveChanges();
            }
            return Json(true);

        }
        #endregion

        #region Order Management

        [Authorize]
        [HttpGet]
        public ActionResult AddVenderOrder(int? page, int? pageSizeValue, string filter, string filterStatus)
        {
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            var objdriver = new List<Order>();
            objdriver = CallOrderList();

            if (filter == null || filter == "")
            {
                ViewBag.filter_status = filterStatus;
                ViewBag.filter_order = filterStatus;
                ViewBag.filter_brand = filterStatus;
            }
            else if (filterStatus != null || filterStatus != "")
            {
                //    objFilterProduct = CallProductList();
                if (filter == "Status")
                {
                    if (filterStatus == "All" || (filterStatus == "" || filterStatus == ""))
                    {
                        // objFilterProduct = objProduct.ToList();
                    }
                    else
                    {
                        objdriver = objdriver.Where(x => x.Status == filterStatus).ToList();
                    }
                    ViewBag.filter_status = filterStatus;
                }
                else if (filter == "OrderCount")
                {
                    //ProductOrder
                    if (filterStatus != "" && filterStatus != "0")
                    {
                        int startCount = Convert.ToInt16(filterStatus.Split('-')[0]);
                        int secondCount = Convert.ToInt16(filterStatus.Split('-')[1]);
                        if (secondCount != 0)
                        {
                            List<int?> ProductIDs = new List<int?>();
                            ProductIDs = db.orders.GroupBy(x => x.product_id).Where(grp => grp.Count() > startCount && grp.Count() < secondCount).Select(x => x.Key).ToList();
                            var obj = from a in objdriver
                                      join p in ProductIDs on a.productid equals (p.HasValue ? p.Value : 0)
                                      select a;
                            objdriver = obj.ToList();
                        }
                        else
                        {
                            List<int?> ProductIDs = new List<int?>();
                            ProductIDs = db.orders.GroupBy(x => x.product_id).Where(grp => grp.Count() > startCount).Select(x => x.Key).ToList();
                            var obj = from a in objdriver
                                      join p in ProductIDs on a.productid equals (p.HasValue ? p.Value : 0)
                                      select a;
                            objdriver = obj.ToList();
                        }
                    }
                    ViewBag.filter_order = filterStatus;
                }
                else if (filter == "Brand")
                {
                    ViewBag.filter_brand = filterStatus;
                }

            }
            return View(objdriver.ToPagedList(pageNumber, pageSize));
        }

        private List<Order> CallOrderList()
        {
            List<Order> objOrder = new List<Order>();
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                int venderID = Convert.ToInt16(Session["VenderUserID"]);
                var OrderQuery = (from ord in db.orders

                                      // join add in db.shippingaddresses on ord.orderid equals add.orderid
                                  join p in db.products on ord.product_id equals p.productid
                                  join v in db.venders on p.vender_id equals v.venderid
                                  join cust in db.customers on ord.customer_id equals cust.customerid
                                  join card in db.customerpaymentcards on cust.customerid equals card.customerid into dcard
                                  from card in dcard.DefaultIfEmpty()
                                  where ord.status != "Deleted" && v.venderid == venderID
                                  select new
                                  {
                                      /*  add.streetaddress,
                                        add.city,
                                        add.country,
                                        add.zip,*/
                                      ord.ship_address,
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
                                      Address = rc.ship_address,
                                      /* Address=rc.streetaddress,
                                       City = rc.city,
                                       Zip = rc.zip,
                                       Country = rc.country,*/
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
                        /*   City = item.City,
                           Country = item.Country,
                           Zip = Convert.ToInt32(item.Zip),*/
                        Status = item.Status
                    });
                }
                return objOrder;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddVenderOrder(Order od, int? page, int? pageSizeValue)
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

        public JsonResult UpdateOrderStatus(int ordId, string sts)
        {
            order ord = new order();
            db.orders.Find(ordId).status = sts;
            var id = ordId;
            ord.status = sts;
            db.SaveChanges();
            return Json(new { orderid = ordId, status = sts });
        }

        [HttpPost]
        public JsonResult DeleteOrder(string[] orderid)
        {
            JsonResult res = new JsonResult();
            //res = cu.Where(x => x.Status == status).ToList();
            if (orderid != null && orderid.Length > 0)
            {
                foreach (string i in orderid)
                {
                    int id = Convert.ToInt16(i);
                    db.orders.Find(id).status = "Deleted";
                }
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }
        #endregion

        #region Driver Rating Management
        [HttpGet]
        public ActionResult VenderDriverRating(int? page, int? pageSizeValue)//
        {
            List<DiverRating> objDiverRating = new List<DiverRating>();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);

            int venderID = Convert.ToInt16(Session["venderID"]);

            try
            {
                var objListDriver = db.drivers.ToList();

                foreach (var listdrivers in objListDriver)
                {

                    var objRatings = (from d in db.drivers
                                      join dr in db.driverratings on d.driverid equals dr.driver_id
                                      //    join img in db.ImageFiles on d.image equals img.ImageId
                                      where d.driverid == listdrivers.driverid && dr.status != "Deleted" && d.vender_id == venderID
                                      select new
                                      {
                                          dusername = d.dusername,
                                          emailid = d.emailid,
                                          comment = dr.comment,
                                          rating = dr.rating,
                                          name = d.name,
                                          image = d.Image_path,
                                          drateid = dr.drateid
                                      }).ToList();

                    foreach (var a in objRatings)
                    {

                        objDiverRating.Add(new DiverRating
                        {
                            dusername = a.dusername,
                            emailid = a.emailid,
                            comment = a.comment,
                            rating = a.rating,
                            name = a.name,
                            driver_image = a.image,
                            drateid = a.drateid
                        });

                    }

                }



            }
            catch (Exception ex)
            {
                objLogError.LogErrorFile(ex);

            }



            return View(objDiverRating.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DriverRatingDetails(int Id)
        {
            DiverRating objDriver = new DiverRating();
            var objRatings = (from d in db.drivers
                              join dr in db.driverratings on d.driverid equals dr.driver_id
                              //  join img in db.ImageFiles on d.image equals img.ImageId
                              where dr.drateid == Id
                              select new
                              {
                                  dusername = d.dusername,
                                  emailid = d.emailid,
                                  comment = dr.comment,
                                  rating = dr.rating,
                                  name = d.name,
                                  image = d.Image_path,
                                  drateid = dr.drateid
                              }).FirstOrDefault();
            if (objRatings != null)
            {
                objDriver.dusername = objRatings.dusername;
                objDriver.emailid = objRatings.emailid;
                objDriver.comment = objRatings.comment;
                objDriver.rating = objRatings.rating;
                objDriver.name = objRatings.name;
                objDriver.driver_image = objRatings.image;
                objDriver.drateid = objRatings.drateid;

            }
            return PartialView("DriverRatingDetails", objDriver);
        }

        [HttpPost]
        public JsonResult VenderDeleteRating(string[] ids)
        {
            JsonResult res = new JsonResult();
            if (ids != null && ids.Length > 0)
            {
                foreach (string i in ids)
                {
                    int id = Convert.ToInt16(i);
                    db.driverratings.Find(id).status = "Deleted";
                }
                db.SaveChanges();
            }
            return Json(true);

        }
        #endregion

        #region Driver Management

        public ActionResult AddVenderDriver(int? page, int? pageSizeValue, string filter, string filterStatus)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                var objdriveruser = new List<Driver>();
                objdriveruser = CallDriverList();
                if (filter == null || filter == "")
                {
                    ViewBag.filter_status = filterStatus;
                }
                else if (filterStatus != null || filterStatus != "")
                {
                    //    objFilterProduct = CallProductList();
                    if (filter == "Status")
                    {
                        if (filterStatus == "0" || (filterStatus == "" || filterStatus == ""))
                        {
                            // objFilterProduct = objProduct.ToList();
                        }
                        else
                        {
                            objdriveruser = objdriveruser.Where(x => x.Status == filterStatus).ToList();
                        }
                        ViewBag.filter_status = filterStatus;
                    }
                }
                return View(objdriveruser.ToPagedList(pageNumber, pageSize));
            }
        }

        private List<Driver> CallDriverList()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                int venderID = Convert.ToInt16(Session["VenderUserID"]);
                List<Driver> objDriver = new List<Driver>();
                var VenderQuery = (from d in db.drivers
                                   where d.status != "Deleted" && d.vender_id == venderID
                                   join s in db.shippinghistories on d.driverid equals s.driver_id into sD
                                   from s in sD.DefaultIfEmpty()
                                   join o in db.orders on s.order_id equals o.orderid into oD
                                   from o in oD.DefaultIfEmpty()
                                   join add in db.driverdefaultaddresses on d.driverid equals add.driverid
                                   join dbd in db.driverbankdetails on d.driverid equals dbd.driver_id into edbd
                                   from dbd in edbd.DefaultIfEmpty()
                                   join dap in db.driveraddressproffs on d.driverid equals dap.driver_id into edap
                                   from dap in edap.DefaultIfEmpty()
                                   join img in db.ImageFiles on d.image equals img.ImageId into iD
                                   from img in iD.DefaultIfEmpty()
                                   group o by new
                                   {
                                       d.name,
                                       d.emailid,
                                       d.registration_date,
                                       d.driverid,
                                       d.mobile_no,
                                       d.status,
                                       d.driver_nationality,
                                       d.driver_phone_type,
                                       d.driver_divice_id,
                                       d.driver_telicom_carrer,
                                       d.gender,
                                       add.streetaddress,
                                       add.drivernote1,
                                       add.drivernote2,
                                       add.city,
                                       add.zip,
                                       add.state,
                                       add.country,
                                       dbd.accountno,
                                       dbd.bank_name,
                                       dbd.branch_name,
                                       dbd.benificary_name_in_bank,
                                       dbd.ifsc_code,
                                       dap.address_image,
                                       img.Imageattachment,
                                       d.Image_path,
                                       d.password
                                   }
                                       into rc
                                   select new
                                   {
                                       id = rc.Key.driverid,
                                       Name = rc.Key.name,
                                       Avatar = rc.Key.Imageattachment,
                                       password = rc.Key.password,
                                       Email = rc.Key.emailid,
                                       RegDate = rc.Key.registration_date,
                                       TotalDelivery = rc.Count(),
                                       Amount = rc.Sum(r => r.total),
                                       Mobile = rc.Key.mobile_no,
                                       Address = rc.Key.streetaddress,
                                       City = rc.Key.city,
                                       Zip = rc.Key.zip,
                                       State = rc.Key.state,
                                       Country = rc.Key.country,
                                       Note1 = rc.Key.drivernote1,
                                       Note2 = rc.Key.drivernote2,
                                       status = rc.Key.status,
                                       Nationality = rc.Key.driver_nationality,
                                       Gender = rc.Key.gender,
                                       deviceID = rc.Key.driver_divice_id,
                                       phoneType = rc.Key.driver_phone_type,
                                       telecomm = rc.Key.driver_telicom_carrer,
                                       accountNo = rc.Key.accountno == null ? 0 : rc.Key.accountno,
                                       bankName = rc.Key.bank_name,
                                       benificaryName = rc.Key.benificary_name_in_bank,
                                       branchName = rc.Key.branch_name,
                                       ifscCode = rc.Key.ifsc_code,
                                       addressProf = rc.Key.address_image,
                                       imageInfo = rc.Key.Image_path,


                                   }).ToList();
                foreach (var item in VenderQuery)
                {
                    objDriver.Add(new Driver()
                    {
                        driverid = item.id,
                        Image = item.Avatar,
                        Name = item.Name,
                        Email = item.Email,
                        Regdate = Convert.ToDateTime(item.RegDate),
                        TotalDelivery = item.TotalDelivery,
                        Amount = item.Amount.HasValue ? Math.Round(item.Amount.Value == null ? 0 : item.Amount.Value, 2) : item.Amount,
                        DriverNote1 = item.Note1,
                        DriverNote2 = item.Note2,
                        Address = item.Address,
                        Mobile = item.Mobile,
                        City = item.City,
                        Zip = Convert.ToInt32(item.Zip),
                        State = item.State,
                        Country = item.Country,
                        Status = item.status,
                        Nationality = item.Nationality,
                        Gender = item.Gender,
                        accountNo = item.accountNo,
                        addressProfImage = item.addressProf,
                        bankName = item.bankName,
                        benificary_name = item.benificaryName,
                        branchName = item.branchName,
                        deviceID = item.deviceID,
                        ifscCode = item.ifscCode,
                        image_path = item.imageInfo,
                        Password = item.password,
                        phoneType = item.phoneType,
                        telecomCarrier = item.telecomm
                    });
                }
                return objDriver;
            }
        }

        [HttpPost]
        public ActionResult AddVenderDriver(Driver dr, HttpPostedFileBase drivInfo, int? page, int? pageSizeValue, HttpPostedFileBase addressProf)
        {
            try
            {
                driver der = new driver();
                using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
                {
                    if (ModelState.IsValid)
                    {
                        ImageFile obj = new ImageFile();

                        if (Request.Files.Count > 0)
                        {
                            int id = 0;
                            if (drivInfo != null && drivInfo.ContentLength > 0)
                            {
                                string fileName = "~/content/images_driver/";
                                string targetFolder = Server.MapPath(fileName);
                                bool exists = System.IO.Directory.Exists(targetFolder);
                                if (!exists)
                                {
                                    System.IO.Directory.CreateDirectory(targetFolder);
                                }
                                string targetPath = Path.Combine(fileName, "driver_info" + dr.driverid + drivInfo.FileName.Substring(drivInfo.FileName.LastIndexOf(".")));
                                string savePath = Path.Combine(targetFolder, "driver_info" + dr.driverid + drivInfo.FileName.Substring(drivInfo.FileName.LastIndexOf(".")));
                                drivInfo.SaveAs(savePath);
                                dr.image_path = targetPath;
                            }
                            if (addressProf != null && addressProf.ContentLength > 0)
                            {
                                string fileName = "~/content/images_driver/";
                                string targetFolder = Server.MapPath(fileName);
                                bool exists = System.IO.Directory.Exists(targetFolder);
                                if (!exists)
                                {
                                    System.IO.Directory.CreateDirectory(targetFolder);
                                }
                                string targetPath = Path.Combine(fileName, "driver_addressProf" + dr.driverid + addressProf.FileName.Substring(addressProf.FileName.LastIndexOf(".")));
                                string savePath = Path.Combine(targetFolder, "driver_addressProf" + dr.driverid + addressProf.FileName.Substring(addressProf.FileName.LastIndexOf(".")));
                                addressProf.SaveAs(savePath);
                                dr.addressProfImage = targetPath;
                            }
                            if (dr.driverid == null || dr.driverid <= 0)
                            {
                                if (!dr.IsDriverMailExist(dr.Email, dr.driverid))
                                {
                                    der.name = dr.Name;
                                    der.dusername = dr.Email;
                                    der.emailid = dr.Email;
                                    der.password = dr.Password;
                                    der.registration_date = DateTime.Now;
                                    der.mobile_no = dr.Mobile;
                                    der.driver_nationality = dr.Nationality;
                                    der.gender = dr.Gender;
                                    der.creation_date = DateTime.Now;
                                    der.driver_phone_type = dr.phoneType;
                                    der.driver_divice_id = dr.deviceID;
                                    der.driver_telicom_carrer = dr.telecomCarrier;
                                    der.createdby = Convert.ToInt32(Session["Adminid"]);
                                    der.Image_path = dr.image_path;
                                    db.drivers.Add(der);
                                    db.SaveChanges();
                                    var did = der.driverid;
                                    driverdefaultaddress dda = new driverdefaultaddress();
                                    dda.driverid = did;
                                    dda.streetaddress = dr.Address;
                                    dda.city = dr.City;
                                    dda.zip = dr.Zip;
                                    dda.state = dr.State;
                                    dda.country = dr.Country;
                                    dda.drivernote1 = dr.DriverNote1;
                                    dda.drivernote2 = dr.DriverNote2;
                                    db.driverdefaultaddresses.Add(dda);
                                    db.SaveChanges();
                                    driverbankdetail dbd = new driverbankdetail();
                                    dbd.accountno = dr.accountNo;
                                    dbd.bank_name = dr.bankName;
                                    dbd.branch_name = dr.branchName;
                                    dbd.benificary_name_in_bank = dr.benificary_name;
                                    dbd.ifsc_code = dr.ifscCode;
                                    dbd.driver_id = did;
                                    db.driverbankdetails.Add(dbd);
                                    db.SaveChanges();
                                    if (dr.addressProfImage != null)
                                    {
                                        driveraddressproff dap = new driveraddressproff();
                                        dap.driver_id = did;
                                        dap.address_image = dr.addressProfImage;
                                        db.driveraddressproffs.Add(dap);
                                        db.SaveChanges();
                                    }
                                    ViewBag.message = "Driver Added Sucessfully.";
                                }
                                else
                                {
                                    ViewBag.driverReturn = dr;
                                    ViewBag.status = 0;
                                    ModelState.AddModelError("", "Vender Already Exist");
                                }
                            }
                            else if (dr.driverid > 0)
                            {
                                driver dredit = db.drivers.First(i => i.driverid == dr.driverid);
                                dredit.name = dr.Name;
                                dredit.dusername = dr.Email;
                                dredit.emailid = dr.Email;
                                dredit.password = dr.Password;
                                dredit.registration_date = DateTime.Now;
                                dredit.mobile_no = dr.Mobile;
                                dredit.driver_nationality = dr.Nationality;
                                dredit.gender = dr.Gender;
                                dredit.driver_divice_id = dr.deviceID;
                                dredit.driver_phone_type = dr.phoneType;
                                dredit.driver_telicom_carrer = dr.telecomCarrier;
                                dredit.Image_path = dr.image_path;
                                if (der.image.HasValue && der.image > 0)
                                    dredit.image = der.image;

                                db.SaveChanges();
                                var driverDefault = db.driverdefaultaddresses.Where(x => x.driverid == dr.driverid).Count();
                                if (driverDefault > 0)
                                {
                                    driverdefaultaddress dda = db.driverdefaultaddresses.First(i => i.driverid == dr.driverid);
                                    dda.streetaddress = dr.Address;
                                    dda.city = dr.City;
                                    dda.zip = dr.Zip;
                                    dda.state = dr.State;
                                    dda.country = dr.Country;
                                    dda.drivernote1 = dr.DriverNote1;
                                    dda.drivernote2 = dr.DriverNote2;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    driverdefaultaddress dda = new driverdefaultaddress();
                                    dda.driverid = dr.driverid;
                                    dda.streetaddress = dr.Address;
                                    dda.city = dr.City;
                                    dda.zip = dr.Zip;
                                    dda.state = dr.State;
                                    dda.country = dr.Country;
                                    dda.drivernote1 = dr.DriverNote1;
                                    dda.drivernote2 = dr.DriverNote2;
                                    db.driverdefaultaddresses.Add(dda);
                                    db.SaveChanges();
                                }
                                var driverBank = db.driverbankdetails.Select(x => x.driver_id == dr.driverid).Count();
                                if (driverBank > 0)
                                {
                                    driverbankdetail edbd = db.driverbankdetails.First(x => x.driver_id == dr.driverid);
                                    edbd.accountno = dr.accountNo;
                                    edbd.bank_name = dr.bankName;
                                    edbd.branch_name = dr.branchName;
                                    edbd.benificary_name_in_bank = dr.benificary_name;
                                    edbd.ifsc_code = dr.ifscCode;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    driverbankdetail edbd = new driverbankdetail();
                                    edbd.accountno = dr.accountNo;
                                    edbd.bank_name = dr.bankName;
                                    edbd.branch_name = dr.branchName;
                                    edbd.benificary_name_in_bank = dr.benificary_name;
                                    edbd.ifsc_code = dr.ifscCode;
                                    db.driverbankdetails.Add(edbd);
                                    db.SaveChanges();
                                }
                                var driverAddressProf = db.driveraddressproffs.Select(x => x.driver_id == dr.driverid).Count();
                                if (driverAddressProf > 0)
                                {
                                    driveraddressproff edap = db.driveraddressproffs.First(x => x.driver_id == dr.driverid);
                                    edap.address_image = dr.addressProfImage;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    if (dr.addressProfImage != null)
                                    {
                                        driveraddressproff edap = db.driveraddressproffs.First(x => x.driver_id == dr.driverid);
                                        edap.address_image = dr.addressProfImage;
                                        db.driveraddressproffs.Add(edap);
                                        db.SaveChanges();
                                    }
                                }
                                ViewBag.message = "Driver Edited Successfully.";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.driverReturn = dr;
                        ViewBag.status = 0;
                        ModelState.AddModelError("", "Vender Already Exist");
                    }
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
            var objdriveruser = new List<Driver>();
            objdriveruser = CallDriverList();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            return View(objdriveruser.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult UpdateDriverStatus(int drvId, string sts)
        {
            driver drv = new driver();
            db.drivers.Find(drvId).status = sts;
            var id = drvId;
            drv.status = sts;
            db.SaveChanges();
            return Json(new { driverid = drvId, status = sts });
        }

        [HttpPost]
        public JsonResult DeleteDriver(string[] ids)
        {
            JsonResult res = new JsonResult();
            if (ids != null && ids.Length > 0)
            {
                foreach (string i in ids)
                {
                    int id = Convert.ToInt16(i);
                    db.drivers.Find(id).status = "Deleted";
                }
                db.SaveChanges();
            }
            return Json(true);

        }

        #endregion

        public ActionResult GetDetail(Vender ve)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var resu = (from ver in db.venders.Where(a => a.venderid.Equals(ve.Venderid)) select ver.venderid).FirstOrDefault();

                var orders = ((from ord in db.orders
                               join prod in db.products on ord.product_id equals prod.productid
                               join ven in db.venders on prod.vender_id equals ven.venderid
                               where ven.venderid == resu
                               select ord.orderid).Count());

                var revenue = ((from ord in db.orders
                                join prod in db.products on ord.product_id equals prod.productid
                                join ven in db.venders on prod.vender_id equals ven.venderid
                                where ven.venderid == resu
                                select ord.total).Sum());

                var pending = ((from ord in db.orders
                                join prod in db.products on ord.product_id equals prod.productid
                                join ven in db.venders on prod.vender_id equals ven.venderid
                                where ord.status == "Pending"
                                select ord.orderid).Count());

                var delivered = ((from ord in db.orders
                                  join prod in db.products on ord.product_id equals prod.productid
                                  join ven in db.venders on prod.vender_id equals ven.venderid
                                  where ord.status == "Delivered"
                                  select ord.orderid).Count());

                var cancel = ((from ord in db.orders
                               join prod in db.products on ord.product_id equals prod.productid
                               join ven in db.venders on prod.vender_id equals ven.venderid
                               where ord.status == "Cancel"
                               select ord.orderid).Count());

                var ontheway = ((from ord in db.orders
                                 join prod in db.products on ord.product_id equals prod.productid
                                 join ven in db.venders on prod.vender_id equals ven.venderid
                                 where ord.status == "On the way"
                                 select ord.orderid).Count());

                var soldout = ((from ord in db.orders
                                join prod in db.products on ord.product_id equals prod.productid
                                join ven in db.venders on prod.vender_id equals ven.venderid
                                where ord.status == "Soldout"
                                select ord.orderid).Count());
            }
            return View();
        }

        #region Geo Fence
        // new code added for geofence
        //for geofence  get methods and post methods 
        [HttpGet]
        public ActionResult geofence(string zonename)
        {
            List<geofences> lstRegions = new List<geofences>();
            List<zone> lstZones = new List<zone>();
            ViewBag.zonename = zonename;
            string regioname = string.Empty;

            if (!string.IsNullOrEmpty(zonename))
            {
                var objzone = (from dg in db.vender_zone
                               where dg.zone_name.Contains(zonename)
                               select new
                               {
                                   dg.zone_name,
                                   dg.vendor_id,
                                   dg.id
                               }).ToList();

                foreach (var item in objzone)
                {
                    lstZones.Add(new zone()
                    {
                        id = item.id,
                        vendorid = item.vendor_id,
                        zone_name = item.zone_name
                    });
                }
            }
            else
            {
                var objzone = (from dg in db.vender_zone

                               select new
                               {
                                   dg.zone_name,
                                   dg.vendor_id,
                                   dg.id
                               }).ToList();

                foreach (var item in objzone)
                {
                    lstZones.Add(new zone()
                    {
                        id = item.id,
                        vendorid = item.vendor_id,
                        zone_name = item.zone_name
                    });
                }

            }
            // Regions
            if (!string.IsNullOrEmpty(regioname))
            {
                var obj = (from dg in db.dgeofences
                           where dg.zone_name.Contains(regioname)
                           select new
                           {
                               dg.geofenceid,
                               dg.zone_name,
                               dg.city_name,
                               dg.direction_name,
                               dg.area,
                               dg.population
                           }).ToList();

                foreach (var item in obj)
                {
                    lstRegions.Add(new geofences()
                    {
                        area = item.area,
                        city_name = item.city_name,
                        cordinates = null,
                        direction_name = item.direction_name,
                        geofenceid = item.geofenceid,
                        population = item.population,
                        zone_name = item.zone_name
                    });
                }
            }
            else
            {

                var obj = (from dg in db.dgeofences

                           select new
                           {
                               dg.geofenceid,
                               dg.zone_name,
                               dg.city_name,
                               dg.direction_name,
                               dg.area,
                               dg.population
                           }).ToList();

                foreach (var item in obj)
                {
                    lstRegions.Add(new geofences()
                    {
                        area = item.area,
                        city_name = item.city_name,
                        cordinates = null,
                        direction_name = item.direction_name,
                        geofenceid = item.geofenceid,
                        population = item.population,
                        zone_name = item.zone_name
                    });
                }

            }

            vendorgeofence objvendorgeofence = new vendorgeofence();
            objvendorgeofence.geofenceslst = lstRegions;
            objvendorgeofence.zonelist = lstZones;
            return View(objvendorgeofence);
        }

        public string AddUpdateZone(string zonename, string id, string geofenceids)
        {

            // Add / update Vender zone 
            var vendorzone = new vender_zone();
            int intid = Convert.ToInt32(id);
            if (intid == 0)
            {
                vendorzone.vendor_id = 1;
                vendorzone.zone_name = zonename;
                vendorzone.created_date = DateTime.Today;
                db.vender_zone.Add(vendorzone);
            }
            else
            {
                vendorzone = db.vender_zone.Where(x => x.id == intid).FirstOrDefault();
                vendorzone.zone_name = zonename;

            }

            db.SaveChanges();

            // add/update venderzone zone mapping
            if (vendorzone.id > 0)
            {
                var venderdeletelist = db.vender_zone_join.Where(x => x.zoneid == vendorzone.id);
                db.vender_zone_join.RemoveRange(venderdeletelist);
                db.SaveChanges();
                var venderzonejoin = new vender_zone_join();

                foreach (string p in geofenceids.Split(','))
                {
                    venderzonejoin = new vender_zone_join();
                    venderzonejoin.zoneid = vendorzone.id;
                    venderzonejoin.geofenceid = Convert.ToInt32(p);
                    db.vender_zone_join.Add(venderzonejoin);
                }
                db.SaveChanges();
            }
            return "1";
        }

        public String getzonefilepath(int zoneid)
        {
            string strreturn = string.Empty;
            string strstart = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><kml xmlns=\"http://earth.google.com/kml/2.1\"><Document><Folder><name>zones</name><Schema name=\"regions\" id=\"zones\"><SimpleField name=\"pkEmirateID\" type=\"int\"></SimpleField><SimpleField name=\"EnglishName\" type=\"string\"></SimpleField><SimpleField name=\"UTMArea\" type=\"float\"></SimpleField><SimpleField name=\"Population\" type=\"int\"></SimpleField></Schema>";
            string strend = "</Folder></Document></kml>";
            string strXML = "<Placemark><name>$#$name$#$</name>	<description>1</description><Style><LineStyle><color>ff0000ff</color></LineStyle><PolyStyle><fill>0</fill></PolyStyle></Style><ExtendedData><SchemaData schemaUrl=\"#regions\"><SimpleData name=\"pkEmirateID\">$#$rateid$#$</SimpleData><SimpleData name=\"EnglishName\">$#$name$#$</SimpleData><SimpleData name=\"UTMArea\">$#$area$#$</SimpleData><SimpleData name=\"Population\">$#$population$#$</SimpleData></SchemaData></ExtendedData><MultiGeometry>$#$cordinates$#$</MultiGeometry></Placemark>";
            var objList = (from dgeo in db.dgeofences join vzj in db.vender_zone_join on dgeo.geofenceid equals vzj.geofenceid where vzj.zoneid == zoneid select dgeo).ToList();

            StringBuilder strmain = new StringBuilder();
            strmain.Append(strstart);
            if (objList != null)
            {
                foreach (var mainitem in objList)
                {
                    string newplacemark = strXML;
                    newplacemark = newplacemark.Replace("$#$rateid$#$", mainitem.geofenceid.ToString());
                    newplacemark = newplacemark.Replace("$#$name$#$", mainitem.zone_name);
                    newplacemark = newplacemark.Replace("$#$area$#$", mainitem.area);
                    newplacemark = newplacemark.Replace("$#$population$#$", mainitem.population.ToString());


                    StringBuilder strb = new StringBuilder();
                    var coridinatesList = (from dgcordinates in db.dgeofencelocations where dgcordinates.geofenceid == mainitem.geofenceid select dgcordinates).ToList();
                    if (coridinatesList != null)
                    {
                        foreach (var item in coridinatesList)
                        {
                            strb.Append("<Polygon><outerBoundaryIs><LinearRing><coordinates>");
                            strb.Append(item.location);
                            strb.Append("</coordinates></LinearRing></outerBoundaryIs></Polygon>");

                        }
                    }
                    newplacemark = newplacemark.Replace("$#$cordinates$#$", strb.ToString());
                    strmain.Append(newplacemark);
                }
                strmain.Append(strend);
                string strsessionid = string.Empty;
                if (Session != null)
                {
                    strsessionid = Session.SessionID.ToString();
                }
                System.IO.File.WriteAllText(Server.MapPath("~/content") + "/data/zone_data_" + strsessionid + ".kml", strmain.ToString());
                strreturn = "zone_data_" + strsessionid + ".kml";
            }
            return strreturn;
        }

        public JsonResult getZoneData(int zoneid)
        {
            var zonedata = new zone();

            var obj = db.vender_zone.Where(x => x.id == zoneid).FirstOrDefault();
            zonedata.id = zoneid;
            zonedata.vendorid = obj.vendor_id;
            zonedata.zone_name = obj.zone_name;

            var objmap = db.vender_zone_join.Where(x => x.zoneid == zoneid).ToList();

            if (objmap != null && objmap.Count() > 0)
            {

                string strgeofences = string.Empty;
                foreach (var vzj in objmap)
                {
                    if (!string.IsNullOrEmpty(strgeofences))
                    {
                        strgeofences = strgeofences + ",";
                    }
                    strgeofences = strgeofences + vzj.geofenceid.ToString();
                }
                zonedata.geofences = strgeofences;
            }
            return Json(zonedata);
        }
        // End of new code geofence. 
        #endregion
    }
}

