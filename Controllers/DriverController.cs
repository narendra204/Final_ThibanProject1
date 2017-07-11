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
                                   join s in db.shippinghistories on d.driverid equals s.driver_id
                                   join o in db.orders on s.order_id equals o.orderid
                                   join add in db.driverdefaultaddresses on d.driverid equals add.driverid
                                   join img in db.ImageFiles on d.image equals img.ImageId
                                   group o by new
                                   {
                                       d.name,
                                       d.emailid,
                                       d.registration_date,
                                       d.driverid,
                                       d.mobile_no,
                                       d.status,
                                       add.streetaddress,
                                       add.drivernote1,
                                       add.drivernote2,
                                       add.city,
                                       add.zip,
                                       img.Imageattachment
                                   }
                                       into rc
                                       select new
                                       {
                                           id = rc.Key.driverid,
                                           Name = rc.Key.name,
                                           Avatar = rc.Key.Imageattachment,
                                           Email = rc.Key.emailid,
                                           RegDate = rc.Key.registration_date,
                                           TotalDelivery = rc.Count(),
                                           Amount = rc.Sum(r => r.total),
                                           Mobile = rc.Key.mobile_no,
                                           Address = rc.Key.streetaddress,
                                           City = rc.Key.city,
                                           Zip = rc.Key.zip,
                                           Note1 = rc.Key.drivernote1,
                                           Note2 = rc.Key.drivernote2,
                                           status = rc.Key.status
                                       }).ToList();
                foreach (var item in VenderQuery)
                {
                    objDriver.Add(new Driver()
                    {
                        Id = item.id,
                        Image = item.Avatar,
                        Name = item.Name,
                        Email = item.Email,
                        Regdate = Convert.ToDateTime(item.RegDate),
                        TotalDelivery = item.TotalDelivery,
                        Amount = item.Amount.HasValue ? Math.Round(item.Amount.Value, 2) : item.Amount,
                        drivernote1 = item.Note1,
                        drivernote2 = item.Note2,
                        address = item.Address,
                        Mobile = item.Mobile,
                        city = item.City,
                        zip = Convert.ToInt32(item.Zip),
                        Status = item.status
                    });
                }
                return objDriver;
            }
        }


        [HttpPost]
        public ActionResult AddDriver(Driver dr, HttpPostedFileBase file, int? page, int? pageSizeValue)
        {
            try
            {
                driver der = new driver();
                using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
                {

                    if (!dr.IsDriverMailExist(dr.Email))
                    {
                        ImageFile obj = new ImageFile();

                        if (Request.Files.Count > 0)
                        {
                            int id = 0;
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
                                    der.image = id;
                                }
                            }
                            der.name = dr.Name;
                            der.dusername = dr.Email;
                            der.emailid = dr.Email;
                            der.password = dr.Password;
                            der.registration_date = DateTime.Now;
                            der.mobile_no = dr.Mobile;
                            db.drivers.Add(der);
                            db.SaveChanges();
                            var did = der.driverid;
                            driverdefaultaddress dda = new driverdefaultaddress();
                            dda.driverid = did;
                            dda.streetaddress = dr.address;
                            dda.city = dr.city;
                            dda.zip = dr.zip;
                            dda.state = dr.state;
                            dda.country = dr.country;
                            dda.drivernote1 = dr.drivernote1;
                            dda.drivernote2 = dr.drivernote2;
                            db.driverdefaultaddresses.Add(dda);
                            db.SaveChanges();
                            ViewBag.message = "Driver Added Sucessfully.";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Driver Already Added.");
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