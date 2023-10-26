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
        public ActionResult Index()
        {
            return View();
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
    }
}