using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        Inventory_Management4Entities db = new Inventory_Management4Entities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DisplayProduct()
        {
            List<Product> list = db.Products.OrderByDescending(x => x.id).ToList();
            return View(list);

        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();

        }
        [HttpPost]
        public ActionResult CreateProduct(Product pro)
        {
            if (!int.TryParse(pro.Product_qnty.ToString(), out int quantity) || quantity < 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be a valid integer and greater than or equal to 0.");
            }
            if (!ModelState.IsValid)
            {
                return View(pro);
            }
            db.Products.Add(pro);
            db.SaveChanges();

            return RedirectToAction("DisplayProduct");
        }
        [HttpGet]

        public ActionResult UpdateProduct(int id)
        {
            Product pr = db.Products.Where(x => x.id == id).SingleOrDefault();
            return View(pr);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id, Product pro)
        {
            Product pr = db.Products.Where(x => x.id == id).SingleOrDefault();
            if (!int.TryParse(pro.Product_qnty.ToString(), out int quantity) || quantity < 0)
            {
                ModelState.AddModelError("Product_qnty", "Quantity must be a valid integer and greater than or equal to 0.");
            }
            if (!ModelState.IsValid)
            {
                return View(pro);
            }
            pr.Product_name = pro.Product_name;
            pr.Product_qnty = pro.Product_qnty;
            db.SaveChanges();

            return RedirectToAction("DisplayProduct");
        }

        [HttpGet]
        public ActionResult ProductDetail(int id)
        {
            Product pro = db.Products.Where(x => x.id == id).SingleOrDefault();
            return View(pro);
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            Product pro = db.Products.Where(x => x.id == id).SingleOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id, Product pro)
        {
            Product p = db.Products.Where(x => x.id == id).SingleOrDefault();
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("DisplayProduct");
        }
    }
}