using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models.viewmodel;
using System.Web.Security;

namespace Final_ThibanProject.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string RequestedUrl = "")
        {
            using (ThibanWaterDBEntities DB = new ThibanWaterDBEntities())
            {
                admin ad = new admin();
                //var s = (from u in DB.admins.Where(a => a.username.Equals(l.Username)) select u.password).FirstOrDefault();
                //var pass = new KeySecure.DES().Decrypt(s);
                //var user = (from dst in DB.admins select dst).FirstOrDefault();
                if (l.Username!=null && l.Password!=null)
                {
                    
                    var user = (from data in DB.admins.Where(a => a.username.Equals(l.Username) && a.password.Equals(l.Password)) select data).FirstOrDefault();
                 /*   var image = (from img in DB.ImageFiles.Where(a => a.ImageId==(user.Image)) select img.Imageattachment).FirstOrDefault();
                    //var image = "D:\Project\Final_ThibanProject\ProfileImage\k3.png";
                    var base64 = Convert.ToBase64String(image);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);*/
                    if (user!=null)
                    {
                        FormsAuthentication.SetAuthCookie(user.username, true);
                        Session["Username"] = user.username;
                        Session["Email"] = user.email;
                        Session["Adminid"] = user.adminid;
                        Session["Image"] = user.Image_Url;
                        Session["Image_URL"] = user.Image_Url;
                        Session["Image_URL"] = user.Image_Url;
                        Session["RoleID"] = user.roleid;
                        if (Url.IsLocalUrl(RequestedUrl))
                        {
                            return Redirect(RequestedUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Login Crediantial Failed");
                    }
                    
                }
            }
            ModelState.Remove("Password");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "MyAccount");
        }
    }
}