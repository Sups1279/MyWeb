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
            if (String.IsNullOrEmpty(BinhLuan) || BinhLuan.Equals("") || BinhLuan.Equals(""))
            {
                return Redirect(url);
            }
            else
            {
                BINH_LUAN bl = new BINH_LUAN();
                bl.MaSP = MaSP;
                bl.MaTK = MaTK;
                bl.BinhLuan = BinhLuan;
                db.BINH_LUANs.InsertOnSubmit(bl);
                db.SubmitChanges();
            }
            return Redirect(url);
        }
        public ActionResult XoaBinhLuan(int MaSP,int ?MaTK,string url)
        {
            BINH_LUAN bn = db.BINH_LUANs.Where(n=>n.MaSP == MaSP && n.MaTK == MaTK).Take(1).SingleOrDefault();
            if(bn != null)
            {
                db.BINH_LUANs.DeleteOnSubmit(bn);
                db.SubmitChanges();
            }

            return Redirect(url);
        }
        public ActionResult SuaBinhLuan(int MaSP,int ? MaTK,string BinhLuan,string url)
        {
            BINH_LUAN bl = new BINH_LUAN();
            bl.MaSP = MaSP;
            bl.MaTK = MaTK;
            bl.BinhLuan = BinhLuan.ToString();
            if(String.IsNullOrEmpty(BinhLuan) || BinhLuan == "" || BinhLuan == " ")
            {
                ViewBag["ErrBinhLuan"] = "Khong duoc rong";
                
            }
            db.SubmitChanges();
            return Redirect(url);
        }
    }
}