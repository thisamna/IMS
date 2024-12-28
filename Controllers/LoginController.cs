using WebApplication1.Models;
using System.Linq;
using System.Web.Mvc;

namespace student_management_system_project.Controllers
{
    public class LoginController : Controller
    {
        
        private Inventory_Management4Entities db = new Inventory_Management4Entities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Registration objchk)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Registrations
                            .FirstOrDefault(a => a.username == objchk.username && a.password == objchk.password);

                if (obj != null)
                {
                    Session["UserID"] = obj.id.ToString();
                    Session["UserName"] = obj.username;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The Username OR Password is Incorrect");
                }
            }

            return View(objchk);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}