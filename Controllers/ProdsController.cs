using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_ThibanProject_api.Models;
//using Final_ThibanProject.Models;
using Final_ThibanProject.Models.DB;
using System.Data.Entity;
namespace Final_ThibanProject_api.Controllers
{
    public class ProdsController : ApiController
    {

       Product_api[] products = new Product_api[]
       {
            new Product_api { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product_api { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product_api { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
       };

        public IEnumerable<Product_api> GetAllProds()
        {
            
            return products;
        }

        public IHttpActionResult GetProds(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public IEnumerable<Final_ThibanProject.Models.VendorViewModel> GetAllVendor()
        {
            ThibanWaterDBEntities db = new ThibanWaterDBEntities();
            return db.venders.Select(x=> new Final_ThibanProject.Models.VendorViewModel { Venderid=x.venderid, emailid=x.emailid, name = x.name }).ToList();
        }

        public int? AddVender(vender _vendor)
        {
            ThibanWaterDBEntities db = new ThibanWaterDBEntities();
            db.venders.Add(_vendor);
            db.SaveChanges();
            return _vendor.venderid;
            
        }
    }
}
