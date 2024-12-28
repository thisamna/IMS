using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegistrationsController : Controller
    {
        private Inventory_Management4Entities db = new Inventory_Management4Entities();

        public ActionResult Index()
        {
            return View(db.Registrations.ToList());
        }

    }
}
