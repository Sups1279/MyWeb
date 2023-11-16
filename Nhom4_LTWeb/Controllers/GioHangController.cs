using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_LTWeb.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        DbMyWebDataContext db = new DbMyWebDataContext();


        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> layGioHang()
        {
            
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                Session["GioHang"] = null;
                if (lstGioHang != null)
                {
                    lstGioHang.Clear();
                }
              
            }
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int maSP,string url)
        {
            Session["DuongDan"] = (@Url.Action("ThemGioHang", "GioHang", new {maSP = maSP,url = url}));
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account", new {url = Session["DuongDan"].ToString() });
            }
            List<GioHang> lstGH = layGioHang();

            GioHang sp = lstGH.Find(n => n.iMasp == maSP);

            if(sp== null)
            {
                sp = new GioHang(maSP);
                lstGH.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstioHang = Session["GioHang"] as List<GioHang>;
            if(lstioHang != null)
            {
                iTongSoLuong = lstioHang.Count();
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double TongTien = 0;
            List<GioHang> lstioHang = Session["GioHang"] as List<GioHang>;
            if (lstioHang != null)
            {
                TongTien = lstioHang.Sum(n=>n.dThanhTien);
            }
            return TongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> listGioHang = layGioHang();
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                Session["GioHang"] = null;
                listGioHang.Clear();
            }
            if (listGioHang.Count()==0)
            {
                return RedirectToAction("Index", "Shop");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaSP(int masp)
        {
            List<GioHang> lst = layGioHang();
            GioHang sp = lst.SingleOrDefault(n=>n.iMasp == masp);
            if (sp != null)
            {
                lst.RemoveAll(n => n.iMasp == masp);
                if (lst.Count == 0)
                {
                    return RedirectToAction("DangNhap", "Account");
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int masp, FormCollection f)
        {
            List<GioHang> lstGioHang = layGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMasp ==masp);

            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["SoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> ds = layGioHang();
            ds.Clear();
            return RedirectToAction("Index", "Shop");
        }
        [HttpGet]

        public ActionResult DatHang()
        {
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            List<GioHang> ds = layGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(ds);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DONHANG ddh = new DONHANG();
            KHACHHANG khach = (KHACHHANG)Session["Username"];
            List<GioHang> ds = layGioHang();
            ddh.MaTK = khach.MaTK;
            ddh.NgayDat = DateTime.Now;
            var NgayGiao = String.Format("{0:MM//dd/yyyy}", f["NgayGiao"]);
            ddh.NgayGiao = DateTime.Parse(NgayGiao);
            ddh.TinhTrangDonHang = "true";
            ddh.DaThanhToan = false;
            db.DONHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            foreach (var item in ds)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaSP = item.iMasp;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.GiaSP = (double)item.dDonGia;
                db.CHITIETDATHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang(FormCollection f)
        {
            
            return View();
        }
    }
}