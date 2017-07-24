using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using System.IO;
using Final_ThibanProject.Models;

namespace Final_ThibanProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        ThibanWaterDBEntities DB = new ThibanWaterDBEntities();
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            int roleID = Convert.ToInt16(Session["RoleID"]);
            var roleMenuI = (from rm in DB.rolemanagements
                             join m in DB.menuitems on rm.menuid equals m.menuid
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
            return View();
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            using (ThibanWaterDBEntities DB = new ThibanWaterDBEntities())
            {
                Admin user = new Admin();
                var id = Convert.ToInt32(Session["Adminid"]);
                admin userdata = (from data in DB.admins.Where(a => a.adminid.Equals(id)) select data).FirstOrDefault();
                var image = (from img in DB.ImageFiles.Where(a => a.ImageId==(userdata.Image)) select img.Imageattachment).FirstOrDefault();
                var base64 = Convert.ToBase64String(image);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

                if (userdata != null)
                {
                    user.email = userdata.email;
                    user.imagename = image;
                    user.username = userdata.username;
                    user.password = userdata.password;
                    user.firstname = userdata.firstname;
                    user.lastname = userdata.lastname;
                    user.gender = userdata.gender;
                    user.mobile = userdata.mobile;
                    user.website = userdata.website;
                    Session["Image"] = imgSrc;
                    if (userdata.gender.Equals("Male", StringComparison.CurrentCultureIgnoreCase))
                    {
                        user.Male = "on";

                    }
                    else
                    {
                        user.Female = "on";
                    }
                    return View(user);
                }

                else
                {
                    ModelState.AddModelError("", "Data Not Available");
                }
            }
            return View(new admin());
        }

        [HttpPost]
        public ActionResult MyProfile(Admin ad, HttpPostedFileBase file)
        {
            ThibanWaterDBEntities db = new ThibanWaterDBEntities();
            var id = Convert.ToInt32(Session["Adminid"]);
            var user = (from da in DB.admins where da.adminid.Equals(id) select da).FirstOrDefault();
            if (user != null)
            {


                if (ModelState.IsValid)
                {
                    user.email = ad.email;
                    user.username = ad.username;
                    //var pass = new KeySecure.DES().Encrypt(ad.password);
                    user.password = ad.password;
                    user.firstname = ad.firstname;
                    user.lastname = ad.lastname;
                    user.gender = ad.gender;
                    user.mobile = ad.mobile;
                    user.website = ad.website;
                    try
                    {
                        ImageFile obj = new ImageFile();
                        int imgid = 0;
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
                                    imgid = obj.ImageId;
                                    ad.Image = imgid;
                                }
                            }
                            user.adminid = id;
                            user.regdate = DateTime.Now;
                            user.status = true;
                            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["Falure"] = ex;
                    }
                }
            }
            else
            {
                TempData["falure"] = "Data Already Exist";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}