using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_LTWeb.Controllers
{
    public class BinhLuanController : Controller
    {
        // GET: BinhLuan
        DbMyWebDataContext db = new DbMyWebDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CommentPartial(int masp,string url) {
            GetALLModel item = new GetALLModel();
            item.GetBINHLUANModels = db.BINH_LUANs.Where(n=>n.MaSP == masp);
            item.GetKHACHHANGModels = db.KHACHHANGs;
            item.GetSANPHAMModels = db.SANPHAMs.Where(n=>n.MaSP==masp);
            return PartialView(item);
        }
        public ActionResult ThemComment(int MaSP,int ?MaTK,string BinhLuan,string url)
        {
            if (MaTK == null)
            {
                return RedirectToAction("DangNhap", "Account", new { url = url });
            }
            BINH_LUAN bl = new BINH_LUAN();
            bl.MaSP = MaSP;
            bl.MaTK = MaTK;
            bl.BinhLuan = BinhLuan;
            db.BINH_LUANs.InsertOnSubmit(bl);
            db.SubmitChanges();
            return Redirect(url);
        }

    }
}