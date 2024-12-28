using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class PurchaseController : Controller
    {
        Inventory_Management4Entities db = new Inventory_Management4Entities();
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DisplayPurchase()
        {
            List<Purchase> list = db.Purchases.OrderByDescending(x => x.id).ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult PurchaseProduct()
        {
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View();
        }
        [HttpPost]
        public ActionResult PurchaseProduct(Purchase pur)
        {
            if (!int.TryParse(pur.Purchase_qnty.ToString(), out int quantity) || quantity < 1)
            {
                ModelState.AddModelError("Quantity", "Quantity must be a valid integer and greater than or equal to 1.");
            }
            if (!ModelState.IsValid)
            {
                var products = db.Products.Select(x => x.Product_name).ToList();
                ViewBag.ProductName = new SelectList(products);
                return View(pur);
            }
            try
            {
                db.Purchases.Add(pur);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var products = db.Products.Select(x => x.Product_name).ToList();
                ViewBag.ProductName = new SelectList(products);
                return View(pur);
            }
            return RedirectToAction("DisplayPurchase");
        }
        public ActionResult Details(int id)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            return View(p);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(int id, Purchase per)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            db.Purchases.Remove(p);
            db.SaveChanges();
            return RedirectToAction("DisplayPurchase");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Purchase p = db.Purchases.SingleOrDefault(x => x.id == id);
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);

            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(int id, Purchase pur)
        {
            if (!int.TryParse(pur.Purchase_qnty.ToString(), out int quantity) || quantity < 0)
            {
                ModelState.AddModelError("Purchase_qnty", "Quantity must be a valid integer and greater than or equal to 1.");
            }
            if (!ModelState.IsValid)
            {
                List<string> list = db.Products.Select(x => x.Product_name).ToList();
                ViewBag.ProductName = new SelectList(list);
                return View(pur);
            }
            Purchase p = db.Purchases.SingleOrDefault(x => x.id == id);
            if (p != null)
            {
                p.Purchase_date = pur.Purchase_date;
                p.Purchase_name = pur.Purchase_name;
                p.Purchase_qnty = pur.Purchase_qnty;
                db.SaveChanges();
            }
            return RedirectToAction("DisplayPurchase");
        }

    }
}