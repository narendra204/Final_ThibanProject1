using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models.LogError;
using Final_ThibanProject.Models.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Final_ThibanProject.Controllers
{
    public class VenderAccountController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        LogError objLogError = new LogError();
        // GET: VenderAccount
        [HttpGet]
        public ActionResult ForgotPassword()
        {

            ForgotPassword objForgotPassword = new ForgotPassword();
            return View(objForgotPassword);

        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword objForgotPassword)
        {
            var status = "";
            Guid activationForgotCode = Guid.NewGuid();
            vender vender = null;
            if (ModelState.IsValid)
            {
                vender = db.venders.Where(a => a.emailid == objForgotPassword.emailid && a.status == "Active").FirstOrDefault();
                if (vender != null)
                {
                    objForgotPassword.venderid = vender.venderid;
                    vender.ForgotActivationId = activationForgotCode;
                    db.SaveChanges();
                    //mail message
                    MailMessage mail = new MailMessage();
                    mail.To.Add(vender.emailid);
                    mail.From = new MailAddress("marlusybanch@gmail.com");
                    mail.Subject = "Activation Mail";
                    //string Body = "Hello, " + vender.name + "\n Your Account is activated please click  the  link  to login \n http://localhost:56348/login ";
                    string Body = "Hello " + vender.name + ",";
                    Body += "<br /><br />Please click the following link to Reset the password";
                    Body += "<br /><a href = '" + string.Format("{0}://{1}/VenderAccount/ActivationForgot/{2}", Request.Url.Scheme, Request.Url.Authority, activationForgotCode) + "'>Click here to reset the password.</a>";
                    Body += "<br /><br />Thanks";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("marlusybanch@gmail.com", "yogita@09");// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    status = "sent";
                    //end message

                }
                else
                {
                    status = "failed";
                }

            }
            else
            {
                return View(objForgotPassword);
            }

            if (status == "sent")
            {
                ViewBag.Sent = "Sent";
                return View(objForgotPassword);
            }
            else
            {
                ViewBag.Sent = "failed";
                return View(objForgotPassword);
            }
            // return View(objForgotPassword);

        }

        //public ActionResult ActivationForgotPass()
        //{
        //    return View();
        //}


        public ActionResult ActivationForgot()
        {
            ChangePassword objChangePassword = new ChangePassword();
            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());
                vender vender = db.venders.Where(a => a.ForgotActivationId == activationCode).FirstOrDefault();
                if (vender != null)
                {
                    vender.Activationid = Guid.Empty;
                    vender.status = "Active";
                    db.SaveChanges();
                    objChangePassword.venderid = vender.venderid;
                    ViewBag.Message = "correctid";
                    return RedirectToAction("ActivationForgotPass", new { user = vender.venderid });
                }
                else
                {
                    return View();
                }


            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult ActivationForgotPass(int user)
        {
            ChangePassword obj = new ChangePassword();
            vender vender = db.venders.Where(a => a.venderid == user).FirstOrDefault();
            if (vender != null)
            {
                obj.venderid = vender.venderid;
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActivationForgotPass(ChangePassword obj)
        {
            vender vender = db.venders.Where(a => a.venderid == obj.venderid).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (vender != null)
                {
                    vender.password = obj.password;
                    db.SaveChanges();
                    return RedirectToAction("VenderLogin", "Vender");
                }
            }
            return View();
        }
       


    }
}