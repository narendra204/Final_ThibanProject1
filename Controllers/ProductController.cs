using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using System.IO;
using Final_ThibanProject.Models.LogError;
using Final_ThibanProject.Models.viewmodel;
using PagedList;
using System.Data.Entity.Validation;
namespace Final_ThibanProject.Controllers
{
    public class ProductController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();
        LogError objLogError = new LogError();
        Product p = new Product();
        // GET: Product
        [HttpGet]
        [Authorize]
        public ActionResult AddProduct(int? page, int? pageSizeValue, string filter, string filterStatus)
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

              
                return View(objProduct.ToPagedList(pageNumber, pageSize));
            }
        }
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
                var productQuery = (from prod in DB.products
                                    where prod.status != "Deleted"
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid
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
                setDropDownVender();
                return objProduct;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddProduct(Product p, HttpPostedFileBase file, int? page, int? pageSizeValue)
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
                                string targetFolder = Server.MapPath("~/content/images_product/");
                                bool exists = System.IO.Directory.Exists(targetFolder);
                                if (!exists)
                                    System.IO.Directory.CreateDirectory(targetFolder);
                                string targetPath = Path.Combine(targetFolder, "prod_" + p.ProductId + file.FileName.Substring(file.FileName.LastIndexOf(".")));
                                flagimg = 1;
                                file.SaveAs(targetPath);
                                prod.Image_path = "~/content/images_product/prod_" + p.ProductId + file.FileName.Substring(file.FileName.LastIndexOf("."));
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
                                prod.createdby = Convert.ToInt32(Session["Adminid"]);
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
                                prod.createdby = Convert.ToInt32(Session["Adminid"]);

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
            return View(objProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ProductRating(int? page, int? pageSizeValue)//
        {

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
                                      where p.productid == listproducts.productid && pr.status!="Deleted"
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

        public ActionResult ProductDetails(int Id)
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
            return PartialView("_ProductDetails", objProductRating);
        }


        public JsonResult UpdateStatus(int prodid, string sts)
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

        [HttpPost]
        public JsonResult DeleteRating(string[] ids)
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
        //********************************************************************************************
        /// <summary>
        /// This is a vender module and we are adding the product from the vender.
        /// </summary>
        /// <returns></returns>
        /// 
        //************************************************************************************

        public ActionResult AddVenderProduct(int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var objvenderprod = new List<VenderProduct>();
                objvenderprod = Callvenderproductlist();
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                return View(objvenderprod.ToPagedList(pageNumber, pageSize));
            }
        }


        //////////////Getting Result for vender product report ////////////
        private List<VenderProduct> Callvenderproductlist()
        {
            List<VenderProduct> objvenderproduct = new List<VenderProduct>();
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var getAllproduct = (from prod in db.products
                                     join img in db.ImageFiles on prod.image equals img.ImageId
                                     group prod by new
                                     {
                                         prod.productid,
                                         prod.product_title,
                                         prod.brand,
                                         prod.sku,
                                         prod.customer_price,
                                         prod.volume,
                                         prod.status,
                                         img.Imageattachment
                                     } into rc
                                     select new
                                     {
                                         Productid = rc.Key.productid,
                                         Image = rc.Key.Imageattachment,
                                         Producttitle = rc.Key.product_title,
                                         Brand = rc.Key.brand,
                                         SKU = rc.Key.sku,
                                         Price = rc.Key.customer_price,
                                         Volumn = rc.Key.volume,
                                         Status = rc.Key.status
                                     }).ToList();
                foreach (var item in getAllproduct)
                {
                    objvenderproduct.Add(new VenderProduct()
                    {
                        productid = item.Productid,
                        productname = item.Producttitle,
                        brandname = item.Brand,
                        sku = item.SKU.ToString(),
                        custprice = item.Price.HasValue ? Math.Round(item.Price.Value, 2) : item.Price,
                        volumn = item.Volumn,
                        status = item.Status,
                        image = item.Image
                    });
                }
            }
            return objvenderproduct;
        }



        [HttpPost]
        public ActionResult AddVenderProduct(VenderProduct vp, HttpPostedFileBase file, int? page, int? pageSizeValue)
        {
            try
            {
                using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
                {
                    product p = new product();
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
                                p.image = id;
                            }
                        }
                        p.product_title = vp.productname;
                        p.brand = vp.brandname;
                        p.volume = vp.volumn;
                        p.bottle_per_box = vp.bottleperbox;
                        p.phno = vp.phno;
                        p.customer_price = vp.custprice;
                        p.store_price = vp.custstoreprice;
                        p.customer_min_order_quantity = vp.custminorder;
                        p.customer_max_order_quantity = vp.custmaxorder;
                        p.store_min_order_quantity = vp.conventionalminorder;
                        p.store_max_order_quantity = vp.conventionalmaxorder;
                        p.description = vp.description;
                        p.status = vp.status;
                        db.products.Add(p);
                        db.SaveChanges();
                        ViewBag.message = "Product Added Sucessfully.";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Product Is Not Added Properly.");
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
            int pageSize = (pageSizeValue ?? 10);
            int pageNumber = (page ?? 1);
            var objvenderproduct = new List<VenderProduct>();
            objvenderproduct = Callvenderproductlist();
            return View(objvenderproduct.ToPagedList(pageNumber, pageSize));
        }
    }
}
