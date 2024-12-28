using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Content
{
    public class RegistrationController : Controller
    {
        Inventory_Management4Entities db = new Inventory_Management4Entities();

            public ActionResult Index()
            {
                return View();
            }

            public ActionResult DisplayRegistrations()
            {
                List<Registration> list = db.Registrations.OrderByDescending(x => x.id).ToList();
                return View(list);
            }

            [HttpGet]
            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
        public ActionResult Create(Registration registration)
        {
            var existingUser = db.Registrations
                                  .Where(r => r.username == registration.username|| r.password == registration.password)
                                  .FirstOrDefault();

            if (existingUser != null)
            {
                if (existingUser.username == registration.username)
                {
                    ModelState.AddModelError("Username", "The username is already taken.");
                }
                if (existingUser.password == registration.password)
                {
                    ModelState.AddModelError("Password", "The password cannot be the same as the username.");
                }
                return View(registration);
            }
            db.Registrations.Add(registration);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
