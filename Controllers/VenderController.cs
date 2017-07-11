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

namespace Final_ThibanProject.Controllers
{
    public class VenderController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        LogError objLogError = new LogError();


        // GET: Vender
        public ActionResult AddVender(int? page, int? pageSizeValue)
        {
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            var objvenderuser = new List<Vender>();
            objvenderuser = CallVenderList();
            return View(objvenderuser.ToPagedList(pageNumber, pageSize));
        }


        private List<Vender> CallVenderList()
        {
            //Fatching data into list object.
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<Vender> objVender = new List<Vender>();
                var VenderQuery = (from v in db.venders
                                   join p in db.products on v.venderid equals p.vender_id
                                   join o in db.orders on p.productid equals o.product_id
                                   join a in db.venderdefaultaddresses on v.venderid equals a.venderid
                                   join img in db.ImageFiles on v.image equals img.ImageId
                                   group o by new
                                   {
                                       v.venderid,
                                       v.name,
                                       v.emailid,
                                       v.registration_date,
                                       v.mobile_no,
                                       v.status,
                                       a.vendernote1,
                                       a.vendernote2,
                                       a.streetaddress,
                                       a.city,
                                       a.zip,
                                       img.Imageattachment
                                   } into rc
                                   select new
                                   {
                                       VenderId = rc.Key.venderid,
                                       Email = rc.Key.emailid,
                                       Avatar = rc.Key.Imageattachment,
                                       Name = rc.Key.name,
                                       MobileNo = rc.Key.mobile_no,
                                       Register = rc.Key.registration_date,
                                       TotalSale = rc.Sum(r => r.total),
                                       Address = rc.Key.streetaddress,
                                       City = rc.Key.city,
                                       Zip = rc.Key.zip,
                                       Status = rc.Key.status
                                   }).ToList();

                //Binding data to a report which is shown on vender view
                foreach (var item in VenderQuery)
                {
                    objVender.Add(new Vender()
                    {
                        Venderid = item.VenderId,
                        image = item.Avatar,
                        name = item.Name,
                        emailid = item.Email,
                        regdate = Convert.ToDateTime(item.Register),
                        totalsale = item.TotalSale.HasValue ? Math.Round(item.TotalSale.Value, 2) : item.TotalSale,
                        mobileno = item.MobileNo,
                        address = item.Address,
                        city = item.City,
                        zip = Convert.ToInt32(item.Zip),
                        Status = item.Status
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
                    if (!ve.IsVenderExist(ve.emailid))
                    {
                        ImageFile obj = new ImageFile();
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
                                ven.image = id;
                            }
                        }
                        ven.name = ve.name;
                        ven.emailid = ve.emailid;
                        ven.username = ve.emailid;
                        ven.registration_date = DateTime.Now;
                        ven.password = ve.password;
                        ven.mobile_no = ve.mobileno;
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
                        ViewBag.message = "Vender Added Sucessfully";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Vender Already Exist");
                    }

                    //////////

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
                MailMessage mail = new MailMessage();
                mail.To.Add(vender.emailid);
                mail.From = new MailAddress("marlusybanch@gmail.com");
                mail.Subject = "Activation Mail";
                //string Body = "Hello, " + vender.name + "\n Your Account is activated please click  the  link  to login \n http://localhost:56348/login ";
                string Body = "Hello " + vender.name + ",";
                Body += "<br /><br />Please click the following link to activate your account";
                Body += "<br /><a href = '" + string.Format("{0}://{1}/Vender/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
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
                        ViewBag.result = "succses";
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
            MailMessage mail = new MailMessage();
            mail.To.Add(vender.emailid);
            mail.From = new MailAddress("marlusybanch@gmail.com");
            mail.Subject = "Activation Mail";
            //string Body = "Hello, " + vender.name + "\n Your Account is activated please click  the  link  to login \n http://localhost:56348/login ";
            string Body = "Hello " + vender.name + ",";
            Body += "<br /><br />Please click the following link to activate your account";
            Body += "<br /><a href = '" + string.Format("{0}://{1}/Vender/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
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
                        ViewBag.VenderLogin = "succses";
                        accepted = vender.Accepted;

                    }
                    else
                    {

                        ViewBag.VenderLogin = "Incorrect";
                        return View(objVenderLogin);

                    }

                }
            }
            catch (Exception ex)
            {
                // ex.Message = "Exception from ProductRating httpget";
                objLogError.LogErrorFile(ex);
                ViewBag.VenderLogin = "failed";
            }
            if (ViewBag.VenderLogin == "succses" && accepted == false)
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
            else if (ViewBag.VenderLogin == "succses" && accepted == true)
            {
                return RedirectToAction("VenderRedirectIndex", "Vender", new { user = vender.venderid });

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

        //private void sendmsg()
        //{
        //    SendSms sms = new SendSms();
        //    string status = sms.send("", "", "", "");
        //    if (status == "1")
        //    {

        //    }
        //    else if (status == "2")
        //    {

        //    }
        //}

        //private void sendmsg()
        //{
        //   string voiceToken = "your-voice-token-here";
        //    string messagingToken = "your-messaging-token-here";

        //    // A collection to hold the parameters we want to send to the Tropo Session API.
        //    IDictionary<string, string> parameters = new Dictionary<String, String>();

        //    // Enter a phone number to send a call or SMS message to here.
        //    parameters.Add("sendToNumber", "+919300485549");

        //    // Enter a phone number to use as the caller ID.
        //    parameters.Add("sendFromNumber", "+919515733547");

        //    // Select the channel you want to use via the Channel struct.
        //    string channel = Channel.Text;
        //    parameters.Add("channel", channel);

        //    string network = Network.SMS;
        //    parameters.Add("network", network);

        //    // Message is sent as a query string parameter, make sure it is properly encoded.
        //    parameters.Add("msg", HttpUtility.UrlEncode("This is a test message from C#."));

        //    // Instantiate a new instance of the Tropo object.
        //    Tropo tropo = new Tropo();

        //    // Create an XML doc to hold the response from the Tropo Session API.
        //    XmlDocument doc = new XmlDocument();

        //    // Set the token to use.
        //    string token = channel == Channel.Text ? messagingToken : voiceToken;

        //    // Load the XML document with the return value of the CreateSession() method call.
        //    doc.Load(tropo.CreateSession(token, parameters));

        //    // Display the results in the console.
        //    Console.WriteLine("Result: " + doc.SelectSingleNode("session/success").InnerText.ToUpper());
        //    Console.WriteLine("Token: " + doc.SelectSingleNode("session/token").InnerText);
        //    Console.Read();
        //   // throw new NotImplementedException();
        //}

        //public ActionResult Access(string user, int otp)
        //{
        //    if (otp == userModel.Otp && user == userModel.Login)
        //    {
        //        TimeSpan timeSub = DateTime.Now - userModel.OtpCrtDate;
        //        if (timeSub.TotalSeconds < 30.0 || userModel.Logged)
        //        {
        //            //LogIn successful
        //            userModel.Logged = true;
        //            return View("../PrivateArea/Account", userModel);
        //        }
        //        else
        //        {
        //            ViewData["status"] = "Sorry but your OTP is very old. Get a new one.";
        //            return View("UserLogin", userModel);
        //        }

        //    }
        //    return LoginFailed();
        //}

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

        //[HttpPost]
        //public ActionResult venderIndex(VenderDetails objVenderDetails,string submit)
        //{
        //    //if (submit == "Verify")
        //    //{
        //    //    vender vender = db.venders.Where(a => a.verifyotp== objVenderDetails.Otp).FirstOrDefault();
        //    //    if (vender != null)
        //    //    {

        //    //        vender.verifyotp = 1234567890;
        //    //        db.SaveChanges();
        //    //        ViewBag.verifyotp = "OTPVerified";

        //    //    }

        //    //    else
        //    //    {
        //    //        ViewBag.verifyotp = "";
        //    //    }

        //    //}
        //    //else if (submit == "Continue")
        //    //{
        //    //    ViewBag.verifyotp = "";
        //    //}
        //   // else if (submit == "I accept")
        //   // {
        //     //   ViewBag.verifyotp = "";
        //  //  }
        //   // else
        //   // {
        //  //      ViewBag.verifyotp = "";
        //  //  }

        //    return View(objVenderDetails);
        //}


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

        //[HttpPost]

        //public ActionResult UserAgreement(VenderAddProff objVenderAddProff)
        //{

        //    venderaddressproof objVenAddProff = new venderaddressproof();
        //    venderbankdetail objVenBankDetail = new venderbankdetail();

        //    try
        //    {

        //          //  vender = db.venders.Where(a => a.username == objVenderLogin.username && a.password == objVenderLogin.password && a.status == true).FirstOrDefault();
        //            //if (vender != null)

        //            objVenAddProff.venderid = objVenderAddProff.venderid;
        //            objVenAddProff.Nationality = objVenderAddProff.Nationality;
        //            objVenAddProff.POBox = objVenderAddProff.POBox;
        //            objVenAddProff.PostalCode = objVenderAddProff.PostalCode;
        //            objVenAddProff.RegistrationDate = objVenderAddProff.RegistrationDate;
        //            objVenAddProff.TlelphoneNumber = objVenderAddProff.RegistrationDate;
        //            objVenAddProff.CompanyTradeName = objVenderAddProff.RegistrationDate;
        //            objVenAddProff.CompanyType = objVenderAddProff.RegistrationDate;
        //            objVenAddProff.CompanyType2 = objVenderAddProff.RegistrationDate;
        //            objVenAddProff.MainOffice = objVenderAddProff.RegistrationDate;
        //            db.venderaddressproofs.Add(objVenAddProff);
        //            db.SaveChanges();
        //            objVenBankDetail.bank_name = objVenderAddProff.RegistrationDate;
        //            objVenBankDetail.HolderName = objVenderAddProff.RegistrationDate;
        //            objVenBankDetail.IBANCode = objVenderAddProff.RegistrationDate;
        //            objVenBankDetail.venderid = objVenderAddProff.venderid;
        //            db.venderbankdetails.Add(objVenBankDetail);
        //            db.SaveChanges();
        //           // objVenAddProff.status = vender.status;
        //          //  objVenAddProff.username = vender.username;
        //            ViewBag.UserAgreement = "succses";             


        //    }
        //    catch (Exception ex)
        //    {
        //        // ex.Message = "Exception from ProductRating httpget";
        //        objLogError.LogErrorFile(ex);
        //        ViewBag.UserAgreement = "failed";
        //    }
        //    if (ViewBag.UserAgreement == "succses")
        //    {
        //       // return View("venderIndex", objVenderDetails);
        //        return View("VenderRedirectIndex");

        //    }
        //    else
        //    {
        //        return View(objVenderAddProff);
        //    }

        //  //  return View(objVenderAddProff);
        //}


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

        [HttpGet]
        public ActionResult VenderProductManagement()
        {
            ///////////
            List<SelectListItem> listBrandName = new List<SelectListItem>();
            listBrandName.Add(new SelectListItem { Text = "Brand Name", Value = "0" });
            var brandName = (from c in db.BrandNames select c).ToArray();
            for (int i = 0; i < brandName.Length; i++)
            {
                listBrandName.Add(new SelectListItem
                {
                    Text = brandName[i].Value,
                    Value = brandName[i].Id.ToString(),
                    Selected = (brandName[i].Id == 0)
                });
            }
            ViewData["BrandName"] = listBrandName;
            //////////////
            List<SelectListItem> listProductAvailability = new List<SelectListItem>();
            listProductAvailability.Add(new SelectListItem { Text = "Product Availability", Value = "0" });
            var prodAvailability = (from c in db.ProductAvailabilities select c).ToArray();
            for (int i = 0; i < prodAvailability.Length; i++)
            {
                listProductAvailability.Add(new SelectListItem
                {
                    Text = prodAvailability[i].Value,
                    Value = prodAvailability[i].Id.ToString(),
                    Selected = (prodAvailability[i].Id == 0)
                });
            }
            ViewData["ProductAvailability"] = listProductAvailability;
            //////////////
            List<SelectListItem> listVolume = new List<SelectListItem>();
            listVolume.Add(new SelectListItem { Text = "Volume", Value = "0" });
            var Volume = (from c in db.Volumes select c).ToArray();
            for (int i = 0; i < Volume.Length; i++)
            {
                listVolume.Add(new SelectListItem
                {
                    Text = Volume[i].Value,
                    Value = Volume[i].Id.ToString(),
                    Selected = (Volume[i].Id == 0)
                });
            }
            ViewData["listVolume"] = listVolume;
            ///////////////
            List<SelectListItem> listBottleMaterial = new List<SelectListItem>();
            listBottleMaterial.Add(new SelectListItem { Text = "Bottle Material", Value = "0" });
            var BottleMaterial = (from c in db.BottleMaterials select c).ToArray();
            for (int i = 0; i < BottleMaterial.Length; i++)
            {
                listBottleMaterial.Add(new SelectListItem
                {
                    Text = BottleMaterial[i].Value,
                    Value = BottleMaterial[i].Id.ToString(),
                    Selected = (BottleMaterial[i].Id == 0)
                });
            }
            ViewData["listBottleMaterial"] = listBottleMaterial;
            /////////////////
            List<SelectListItem> listBottlePerBox = new List<SelectListItem>();
            listBottlePerBox.Add(new SelectListItem { Text = "Bottle Per Box", Value = "0" });
            var BottlePerBox = (from c in db.BottlePerBoxes select c).ToArray();
            for (int i = 0; i < BottlePerBox.Length; i++)
            {
                listBottlePerBox.Add(new SelectListItem
                {
                    Text = BottlePerBox[i].Value,
                    Value = BottlePerBox[i].Id.ToString(),
                    Selected = (BottlePerBox[i].Id == 0)
                });
            }
            ViewData["listBottlePerBox"] = listBottlePerBox;
            ////////////////
            List<SelectListItem> listPhNumber = new List<SelectListItem>();
            listPhNumber.Add(new SelectListItem { Text = "PH Number", Value = "0" });
            var phNumber = (from c in db.PHNumbers select c).ToArray();
            for (int i = 0; i < phNumber.Length; i++)
            {
                listPhNumber.Add(new SelectListItem
                {
                    Text = phNumber[i].Value,
                    Value = phNumber[i].Id.ToString(),
                    Selected = (phNumber[i].Id == 0)
                });
            }
            ViewData["listPhNumber"] = listPhNumber;
            /////////////////
            List<SelectListItem> listCustomerMinimumOrder = new List<SelectListItem>();
            listCustomerMinimumOrder.Add(new SelectListItem { Text = "Customer Minimum Order", Value = "0" });
            var customerMinimumOrder = (from c in db.CustomerMinimumOrders select c).ToArray();
            for (int i = 0; i < customerMinimumOrder.Length; i++)
            {
                listCustomerMinimumOrder.Add(new SelectListItem
                {
                    Text = customerMinimumOrder[i].Value,
                    Value = customerMinimumOrder[i].Id.ToString(),
                    Selected = (customerMinimumOrder[i].Id == 0)
                });
            }
            ViewData["CustomerMinimumOrder"] = listCustomerMinimumOrder;
            //////////////////
            List<SelectListItem> listConventionalStoreMinimumOrder = new List<SelectListItem>();
            listConventionalStoreMinimumOrder.Add(new SelectListItem { Text = "Conventional store Minimum Order", Value = "0" });
            var ConventionalStoreMinimumOrder = (from c in db.ConventionalstoreMinimumOrders select c).ToArray();
            for (int i = 0; i < ConventionalStoreMinimumOrder.Length; i++)
            {
                listConventionalStoreMinimumOrder.Add(new SelectListItem
                {
                    Text = ConventionalStoreMinimumOrder[i].Value,
                    Value = ConventionalStoreMinimumOrder[i].Id.ToString(),
                    Selected = (ConventionalStoreMinimumOrder[i].Id == 0)
                });
            }
            ViewData["listConventionalStoreMinimumOrder"] = listConventionalStoreMinimumOrder;
            //////////////////
            List<SelectListItem> listCustomerMaximumOrderQuality = new List<SelectListItem>();
            listCustomerMaximumOrderQuality.Add(new SelectListItem { Text = "Customer Maximum Order Quality", Value = "0" });
            var cusMaxOrdQual = (from c in db.CustomerMaximumOrderQualities select c).ToArray();
            for (int i = 0; i < cusMaxOrdQual.Length; i++)
            {
                listCustomerMaximumOrderQuality.Add(new SelectListItem
                {
                    Text = cusMaxOrdQual[i].Value,
                    Value = cusMaxOrdQual[i].Id.ToString(),
                    Selected = (cusMaxOrdQual[i].Id == 0)
                });
            }
            ViewData["listCustomerMaximumOrderQuality"] = listCustomerMaximumOrderQuality;
            /////////////////
            List<SelectListItem> listConStrMaxOdrQty = new List<SelectListItem>();
            listConStrMaxOdrQty.Add(new SelectListItem { Text = "Conventional store Miximum Order Quality", Value = "0" });
            var conStrMaxOdrQty = (from c in db.ConventionalstoreMiximumOrderQualities select c).ToArray();
            for (int i = 0; i < conStrMaxOdrQty.Length; i++)
            {
                listConStrMaxOdrQty.Add(new SelectListItem
                {
                    Text = conStrMaxOdrQty[i].Value,
                    Value = conStrMaxOdrQty[i].Id.ToString(),
                    Selected = (conStrMaxOdrQty[i].Id == 0)
                });
            }
            ViewData["listConStrMaxOdrQty"] = listConStrMaxOdrQty;
            /////////////////
            List<SelectListItem> listAverageCompositionPPM = new List<SelectListItem>();
            listAverageCompositionPPM.Add(new SelectListItem { Text = "Conventional store Miximum Order Quality", Value = "0" });
            var averageCompositionPPM = (from c in db.AverageCompositionPPMs select c).ToArray();
            for (int i = 0; i < averageCompositionPPM.Length; i++)
            {
                listAverageCompositionPPM.Add(new SelectListItem
                {
                    Text = averageCompositionPPM[i].Value,
                    Value = averageCompositionPPM[i].Id.ToString(),
                    Selected = (averageCompositionPPM[i].Id == 0)
                });
            }
            ViewData["listAverageCompositionPPM"] = listAverageCompositionPPM;
            /////////////////////

            VenderProductManagement objVenderProductManagement = new VenderProductManagement();
            return View(objVenderProductManagement);
        }

        [HttpPost]
        public ActionResult VenderProductManagement(VenderProductManagement objVenderProductManagement)
        {
            return View(objVenderProductManagement);
        }

        public ActionResult GetDetail(Vender ve)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var resu=(from ver in db.venders.Where(a=>a.venderid.Equals(ve.Venderid))select ver.venderid).FirstOrDefault();

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

    }
}

