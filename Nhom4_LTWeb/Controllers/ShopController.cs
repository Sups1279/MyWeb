using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_LTWeb.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=LAPTOP-VC5IF5QK\\SQLEXPRESS;Initial Catalog=ComputerMuda;Integrated Security=True");

        public ActionResult Index()
        {
            var SP = from c in db.SANPHAMs select c;
            return View(SP.ToList());
        }
        public ActionResult HeaderWebPartial()
        {
            return PartialView();
        }
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult NavQCPartial()
        {
            return PartialView();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult ItemPartial()
        {
            var i = from c in db.SANPHAMs select c;
            return PartialView(i.ToList());
        }
    }
}