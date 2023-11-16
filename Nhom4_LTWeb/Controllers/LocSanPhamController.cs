using Microsoft.Ajax.Utilities;
using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_LTWeb.Controllers
{
    public class LocSanPhamController : Controller
    {
        // GET: LocSanPham
        DbMyWebDataContext db = new DbMyWebDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SanDuLieu(int MaLSP = 0, int Mahang = 0, int RAM = 0, int CPU = 0, int SSD = 0, int HHD = 0, int MAINBOARD = 0,string TenSP = null)
        {
            GetALLModel item = new GetALLModel();
            item.GetSANPHAMModels = db.SANPHAMs;
            if (!String.IsNullOrEmpty(TenSP))
            {
                item.GetSANPHAMModels = db.SANPHAMs.Where(n=>n.TenSP.Contains(TenSP));
            }
            
            
            List<int> l = new List<int>();
            l.Add(RAM);
            l.Add(CPU);
            l.Add(SSD);
            l.Add(MAINBOARD);
            l.Add(HHD);
            if (MaLSP != 0 && Mahang != 0)
            {
                item.GetSANPHAMModels = db.SANPHAMs.Where(n => n.MaHang == Mahang && n.MaLSP == MaLSP);

            }
            else if (MaLSP != 0)
            {
                item.GetSANPHAMModels = db.SANPHAMs.Where(n => n.MaLSP == MaLSP);
            }
            else if (Mahang != 0)
            {
                item.GetSANPHAMModels = db.SANPHAMs.Where(n => n.MaHang == Mahang);
            }
            int tongChon = 0;
             foreach (int i in l)
            {
                if (i > 0)
                {
                    tongChon++;
                }

            }

            var kq = from n in db.CHITIET_SPs
                     group n by new { n.MaSP } into g
                     select new ChiTietSP
                     {
                         id = g.Key.MaSP,
                         count = g.Count()

                     };
            item.GetChiTietSPModels = kq;
            if (tongChon != 0)
            {
                kq = from n in db.CHITIET_SPs
                         where (n.MaThongSo == RAM || n.MaThongSo == CPU || n.MaThongSo == SSD || n.MaThongSo == MAINBOARD)
                         group n by new { n.MaSP } into g
                         select new ChiTietSP
                         {
                             id = g.Key.MaSP,
                             count = g.Count()

                         };
                item.GetChiTietSPModels = kq.Where(n => n.count == tongChon);
            }
            item.GetHANGModels = db.HANGs;
            item.GetsLOAISPModels = db.LOAISPs;
            item.GetTHE_CHITIETModels = db.THE_CHITIETs;
            item.GetTHEModels = db.THEs;
            return View(item);
        }
    }
}