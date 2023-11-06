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
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> layGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int maSP,string url)
        {
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

            }
        }
        public ActionResult GioHang()
        {
            List<GioHang> listGioHang = layGioHang();
            if (listGioHang.Count()==0)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            return View(listGioHang);
        }
    }
}