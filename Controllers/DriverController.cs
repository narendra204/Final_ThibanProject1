using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using System.IO;
using System.Net;
using Final_ThibanProject.Models.LogError;
using Final_ThibanProject.Models.viewmodel;
using PagedList;
using System.Data.Entity.Validation;


namespace Final_ThibanProject.Controllers
{
    public class DriverController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        LogError objLogError = new LogError();
        // GET: Driver
        public ActionResult AddDriver(int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                var objdriveruser = new List<Driver>();
                objdriveruser = CallDriverList();
                return View(objdriveruser.ToPagedList(pageNumber, pageSize));
            }
        }


        private List<Driver> CallDriverList()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {

                List<Driver> objDriver = new List<Driver>();
                var VenderQuery = (from d in db.drivers
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
                                           State=rc.Key.state,
                                           Country=rc.Key.country,
                                           Note1 = rc.Key.drivernote1,
                                           Note2 = rc.Key.drivernote2,
                                           status = rc.Key.status,
                                           Nationality=rc.Key.driver_nationality,
                                           Gender=rc.Key.gender,
                                           deviceID = rc.Key.driver_divice_id,
                                           phoneType =rc.Key.driver_phone_type,
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
                        Amount = item.Amount.HasValue ? Math.Round(item.Amount.Value, 2) : item.Amount,
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
        public ActionResult AddDriver(Driver dr, HttpPostedFileBase drivInfo, int? page, int? pageSizeValue,HttpPostedFileBase addressProf)
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
                                    if(dr.addressProfImage != null) { 
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
                                    if(dr.addressProfImage != null) { 
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

        //Get
        public PartialViewResult GetDriver(int? Id)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var result = from r in db.drivers where r.driverid == Id select r;
                return PartialView("AddDriver");
            }
        }

        [HttpGet]
        public ActionResult DriverRating(int? page, int? pageSizeValue)//
        {
            List<DiverRating> objDiverRating = new List<DiverRating>();
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);



            try
            {
                var objListDriver = db.drivers.ToList();

                foreach (var listdrivers in objListDriver)
                {

                    var objRatings = (from d in db.drivers
                                      join dr in db.driverratings on d.driverid equals dr.driver_id
                                      join img in db.ImageFiles on d.image equals img.ImageId
                                      where d.driverid == listdrivers.driverid
                                      select new
                                      {
                                          dusername = d.dusername,
                                          emailid = d.emailid,
                                          comment = dr.comment,
                                          rating = dr.rating,
                                          name = d.name,
                                          image = img.Imageattachment,
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
                            image = a.image,
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

        public ActionResult Details(int Id)
        {
            DiverRating objDriver = new DiverRating();
            var objRatings = (from d in db.drivers
                              join dr in db.driverratings on d.driverid equals dr.driver_id
                              join img in db.ImageFiles on d.image equals img.ImageId
                              where dr.drateid == Id
                              select new
                              {
                                  dusername = d.dusername,
                                  emailid = d.emailid,
                                  comment = dr.comment,
                                  rating = dr.rating,
                                  name = d.name,
                                  image = img.Imageattachment,
                                  drateid = dr.drateid
                              }).FirstOrDefault();
            if (objRatings != null)
            {
                objDriver.dusername = objRatings.dusername;
                objDriver.emailid = objRatings.emailid;
                objDriver.comment = objRatings.comment;
                objDriver.rating = objRatings.rating;
                objDriver.name = objRatings.name;
                objDriver.image = objRatings.image;
                objDriver.drateid = objRatings.drateid;

            }
            return PartialView("_Details", objDriver);
        }


        public JsonResult UpdateStatus(int drvId, string sts)
        {
            driver drv = new driver();
            db.drivers.Find(drvId).status = sts;
            var id = drvId;
            drv.status = sts;
            db.SaveChanges();
            return Json(new { driverid = drvId, status = sts });
        }


    }
}