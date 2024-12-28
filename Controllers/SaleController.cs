using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SaleController : Controller
    {
        Inventory_Management4Entities db = new Inventory_Management4Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DisplaySale()
        {
            List<Sale> list = db.Sales.OrderByDescending(x => x.id).ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult SaleProduct()
        {
            var products = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(products);
            return View();
        }

        [HttpPost]
        public ActionResult SaleProduct(Sale s)
        {
            if (!int.TryParse(s.Sale_qnty.ToString(), out int quantity))
            {
                ModelState.AddModelError("Sale_qnty", "Quantity must be a valid integer.");
            }
            if (!ModelState.IsValid)
            {
                var products = db.Products.Select(x => x.Product_name).ToList();
                ViewBag.ProductName = new SelectList(products);
                return View(s);
            }

            try
            {
                db.Sales.Add(s);
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
                return View(s);
            }

            return RedirectToAction("DisplaySale");
        }

        public ActionResult Details(int id)
        {
            Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
            return View(s);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
            return View(s);
        }
        [HttpPost]
        public ActionResult Delete(int id, Sale s)
        {
            Sale sale = db.Sales.Where(x => x.id == id).SingleOrDefault();
            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("DisplaySale");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
            var products = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(products);
            return View(s);
        }

        [HttpPost]
        public ActionResult Edit(int id, Sale sale)
        {
            if (!int.TryParse(sale.Sale_qnty.ToString(), out int quantity))
            {
                ModelState.AddModelError("Sale_qnty", "Quantity must be a valid integer.");
            }
            if (!ModelState.IsValid)
            {
                var products = db.Products.Select(x => x.Product_name).ToList();
                ViewBag.ProductName = new SelectList(products);
                return View(sale);
            }
            try
            {
                Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
                s.Sale_date = sale.Sale_date;
                s.Sale_name = sale.Sale_name;
                s.Sale_qnty = sale.Sale_qnty;

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
                return View(sale);
            }
            return RedirectToAction("DisplaySale");
        }

    }
}