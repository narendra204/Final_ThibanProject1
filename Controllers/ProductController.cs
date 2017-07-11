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
        public ActionResult AddProduct(int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<ProductRating> objProductRating = new List<ProductRating>();
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);

                var objProduct = new List<Product>();
                objProduct = CallProductList();
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


                returnList = DB.venders.Select(x =>
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
                                    join ve in DB.venders on prod.vender_id equals ve.venderid
                                    join ca in DB.categories on prod.category_id equals ca.categoryid
                                    join img in DB.ImageFiles on prod.image equals img.ImageId
                                    group prod by new
                                    {
                                        prod.productid,
                                        prod.vender_id,
                                        prod.description,
                                        prod.customer_price,
                                        prod.stock,
                                        prod.product_title,
                                        prod.sku,
                                        prod.status,
                                        ca.category_name,
                                        ve.name,
                                        img.Imageattachment
                                    }
                                        into rc
                                        select new
                                        {
                                            ProdId = rc.Key.productid,
                                            ProductTitle = rc.Key.product_title,
                                            Description = rc.Key.description,
                                            Image = rc.Key.Imageattachment,
                                            ProdPrice = rc.Key.customer_price,
                                            SKU = rc.Key.sku,
                                            Stock = rc.Key.stock,
                                            Status = rc.Key.status,
                                            categoryname = rc.Key.category_name,
                                            Vendername = rc.Key.name
                                        }).ToList();
                foreach (var item in productQuery)
                {
                    objProduct.Add(new Product()
                    {
                        categoryname = item.categoryname,
                        ProductId = item.ProdId,
                        Description = item.Description,
                        Image = item.Image,
                        Title = item.ProductTitle,
                        ProductSKU = item.SKU,
                        productprice = item.ProdPrice.HasValue ? Math.Round(item.ProdPrice.Value, 2) : item.ProdPrice,
                        Availability = item.Stock,
                        Status = item.Status
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
                                    prod.image = id;
                                }
                            }
                            prod.product_title = p.Title;
                            prod.description = p.Description;
                            prod.category_id = p.Category;
                            prod.stock = p.Availability;
                            prod.customer_price = p.productprice;
                            prod.vender_id = p.VenderSource;
                            prod.sku = p.ProductSKU;

                            DB.products.Add(prod);
                            DB.SaveChanges();
                            ViewBag.message = "Product Added Sucessfully.";
                        }
                        else
                        {
                            ModelState.AddModelError("", "Product Already Added.");
                        }
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
                                      where p.productid == listproducts.productid
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
                                         Productid=rc.Key.productid,
                                         Image=rc.Key.Imageattachment,
                                         Producttitle=rc.Key.product_title,
                                         Brand=rc.Key.brand,
                                         SKU=rc.Key.sku,
                                         Price=rc.Key.customer_price,
                                         Volumn=rc.Key.volume,
                                         Status=rc.Key.status
                                     }).ToList();
                foreach (var item in getAllproduct)
                {
                    objvenderproduct.Add(new VenderProduct()
                    {
                        productid=item.Productid,
                        productname=item.Producttitle,
                        brandname=item.Brand,
                        sku=item.SKU.ToString(),
                        custprice=item.Price.HasValue?Math.Round(item.Price.Value,2):item.Price,
                        volumn=item.Volumn,
                        status=item.Status,
                        image=item.Image
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
            return View(objvenderproduct.ToPagedList(pageNumber,pageSize));
        }
    }
}
