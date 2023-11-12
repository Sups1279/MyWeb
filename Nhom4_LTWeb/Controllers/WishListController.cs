using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_LTWeb.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=.;Initial Catalog=ComputerMuda;Integrated Security=True");

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThemWishList(int masp,string url)
        {

            Session["DuongDan"] = (@Url.Action("ThemWishList", "WishList", new { masp = masp, url = url }));
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account", new {url = Session["DuongDan"].ToString() });
            }
            WISHLIST w = new WISHLIST();
            KHACHHANG kh = (KHACHHANG)Session["Username"];
            if(db.WISHLISTs.Where(n=>n.MaSP == masp && n.MaTK == kh.MaTK).Count() > 1)
            {
                return Redirect(url);
            }
            w.MaTK = kh.MaTK;
            w.MaSP = masp;
            w.NgayThemSP = DateTime.Now;
            w.GiaSP = db.SANPHAMs.Where(n=>n.MaSP == masp).Select(n => n.GiaSP).SingleOrDefault();
            db.WISHLISTs.InsertOnSubmit(w);
            db.SubmitChanges();
            GetALLModel item = new GetALLModel();
            item.GetWISHLISTModels = db.WISHLISTs.Where(n => n.MaTK == kh.MaTK);
            ViewBag.TongWL = db.WISHLISTs.Where(n=>n.MaTK==kh.MaTK).Count();
             return Redirect(url);
        }
        public ActionResult WishList()
        {
            KHACHHANG kh = (KHACHHANG)Session["Username"];
            GetALLModel item = new GetALLModel();
            
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account", new { url = Url.Action("WishList", "WishList") });
            }
            if (db.WISHLISTs.Where(n => n.MaTK == kh.MaTK).Count() == 0)
            {
                return RedirectToAction("Index", "Shop");
            }
            item.GetWISHLISTModels = db.WISHLISTs.Where(n => n.MaTK == kh.MaTK);
            item.GetSANPHAMModels = db.SANPHAMs;
            return View(item);  
        }
        public ActionResult WishListPartial()
        {
            return PartialView();
        }
        public ActionResult XoaWishList(int masp)
        {
            KHACHHANG kh = (KHACHHANG)Session["Username"];
            WISHLIST wl = db.WISHLISTs.Where(n => n.MaSP == masp && kh.MaTK == n.MaTK).Take(1).SingleOrDefault();
            db.WISHLISTs.DeleteOnSubmit(wl);
            db.SubmitChanges();
            return RedirectToAction("WishList","WishList");
        }

    }
}