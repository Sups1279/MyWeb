using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom4_LTWeb.Models;

namespace Nhom4_LTWeb.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=A-Khang;Initial Catalog=ComputerMuda;Integrated Security=True");

        public ActionResult Index()
        {
            GetALLModel dulieu = new GetALLModel();
            dulieu.GetSANPHAMModels = db.SANPHAMs.ToList();
            dulieu.GetHANGModels = db.HANGs.ToList();   
            dulieu.GetsLOAISPModels = db.LOAISPs.ToList();
            dulieu.GetHANG_LOAISPModels = db.HANG_LOAISPs.ToList();
            
            return View(dulieu);
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
            return PartialView();
        }
    }
}