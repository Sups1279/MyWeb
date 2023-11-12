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
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=.;Initial Catalog=ComputerMuda;Integrated Security=True");

        public ActionResult Index()
        {
            GetALLModel dulieu = new GetALLModel();
            KHACHHANG kh = (KHACHHANG)Session["Username"];
            int temp = 0;
            if(kh != null)
            {
                temp = kh.MaTK;
            }
            dulieu.GetSANPHAMModels = db.SANPHAMs.ToList();
            dulieu.GetHANGModels = db.HANGs.ToList();   
            dulieu.GetsLOAISPModels = db.LOAISPs.ToList();
            dulieu.GetHANG_LOAISPModels = db.HANG_LOAISPs.ToList();
            dulieu.GetWISHLISTModels = db.WISHLISTs.Where(n=>n.MaTK == temp);
            return View(dulieu);
        }
        public ActionResult Details(int masp)
        {
            GetALLModel dulieu = new GetALLModel();
            dulieu.GetSANPHAMModels = db.SANPHAMs.Where(n=>n.MaSP == masp).ToList();
            dulieu.GetHANGModels = db.HANGs.ToList();
            dulieu.GetsLOAISPModels = db.LOAISPs.ToList();
            dulieu.GetHANG_LOAISPModels = db.HANG_LOAISPs.ToList();
            dulieu.GetCHITIET_SPModels =db.CHITIET_SPs.ToList();
            dulieu.GetTHE_CHITIETModels = db.THE_CHITIETs.ToList();
            dulieu.GetTHEModels = db.THEs.ToList();
            return View(dulieu);
        }
        public ActionResult HeaderWebPartial()
        {
            return PartialView();
        }
        public ActionResult NavPartial()
        {
            GetALLModel item = new GetALLModel();
            item.GetHANGModels = db.HANGs;
            item.GetHANG_LOAISPModels = db.HANG_LOAISPs;
            item.GetTHEModels = db.THEs;
            item.GetTHE_CHITIETModels = db.THE_CHITIETs;
            item.GetsLOAISPModels = db.LOAISPs;
            return PartialView(item);
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