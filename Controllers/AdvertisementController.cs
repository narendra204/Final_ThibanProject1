using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models.viewmodel;
using Final_ThibanProject.Models;
using System.IO;
using System.Data.Entity;

namespace Final_ThibanProject.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: Advertusement
        [HttpGet]
        public ActionResult AddAdvertisement()
        {
            var advertisementdata = new List<Advertisement>();
            advertisementdata = Calladvertisementlist();
            return View(advertisementdata);
        }

        private List<Advertisement> Calladvertisementlist()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<Advertisement> objadvertisement = new List<Advertisement>();
                var advertisementquery = (from adv in db.advertisements select adv).ToList();
                foreach (var item in advertisementquery)
                {
                    objadvertisement.Add(new Advertisement()
                    {
                        Id=item.id,
                        Title = item.name,
                        Type = item.type,
                        Description = item.description,
                    });
                }
                return objadvertisement;
            }
        }


        [HttpGet]
        public ActionResult NewAdvertisement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAdvertisement(Advertisement Ad, HttpPostedFileBase file, string actiontype)
        {
            advertisement adver = new advertisement();
            if (ModelState.IsValid)
            {
                if (actiontype == "Save")
                {
                    using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
                    {
                        if (Request.Files.Count > 0)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var filename = Path.GetFileName(file.FileName);
                                var path = Path.Combine(Server.MapPath("~/ProfileImage/"), filename);
                                file.SaveAs(path);
                                adver.video_image = System.Text.Encoding.Unicode.GetBytes(Url.Content("~/ProfileImage" + filename));
                            }
                            adver.name = Ad.Title;
                            adver.type = Ad.Type;
                            adver.description = Ad.Description;
                            db.advertisements.Add(adver);
                            db.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                return View();
            }
            return View();
        }


        [HttpGet]
        public ActionResult CouponList()
        {
            var coupondata = new List<Coupon>();
            coupondata = Callcouponlist();
            return View(coupondata);
        }

        private List<Coupon> Callcouponlist()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<Coupon> objcoupons = new List<Coupon>();
                var couponquery = (from coupondata in db.coupons select coupondata).ToList();
                foreach (var item in couponquery)
                {
                    objcoupons.Add(new Coupon()
                    {
                        Couponid=item.couponid,
                        couponcode = item.coupon_code,
                        discount = Convert.ToInt32(item.discount),
                        validstartdate = Convert.ToDateTime(item.coupon_valid_start_date),
                        validenddate = Convert.ToDateTime(item.coupon_valid_end_date),
                        status = Convert.ToBoolean(item.coupon_status)
                    });
                }
                return objcoupons;
            }
        }

        public ActionResult AddCoupon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCoupon(Coupon cp)
        {
            if (ModelState.IsValid)
            {
                using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
                {
                    coupon cpl = new coupon();
                    cpl.coupon_code = cp.couponcode;
                    cpl.discount = cp.discount;
                    cpl.coupon_valid_start_date = cp.validstartdate;
                    cpl.coupon_valid_end_date = cp.validenddate;
                    cpl.coupon_status = true;
                    db.coupons.Add(cpl);
                    db.SaveChanges();
                }
            }
            return View();
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ThibanWaterDBEntities db=new ThibanWaterDBEntities())
            {
                advertisement main = db.advertisements.Find(id);
                db.advertisements.Remove(main);
                db.SaveChanges();
            }
           
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult DeleteCoupon(int id)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                coupon main = db.coupons.Find(id);
                db.coupons.Remove(main);
                db.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }


        [HttpPost]
        public JsonResult UpdateData(Models.Coupon coupondata,int id)
        {
            using (ThibanWaterDBEntities db=new ThibanWaterDBEntities())
            {
                coupon result = db.coupons.Find(id);
                return Json(result,JsonRequestBehavior.AllowGet);
            }
        }
    }
}