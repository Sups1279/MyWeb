using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom4_LTWeb.Models
{
    public class GioHang
    {
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=LAPTOP-VC5IF5QK\\SQLEXPRESS;Initial Catalog=ComputerMuda;Integrated Security=True");

        public int iMasp { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public int iSoLuong { get; set; }
        public double dDonGia { get; set; }
        public double dThanhTien { get; set; }
        public int mLSP { get; set; }
        public int mHang { get; set; }

        public GioHang(int mSP)
        {
            iMasp = mSP;
            SANPHAM s = db.SANPHAMs.Where(n => n.MaSP == mSP).SingleOrDefault();
            sTenSP = s.TenSP;
            sAnhBia = s.HinhAnh;
            dDonGia = (double)s.GiaSP;
            dThanhTien = dDonGia * iSoLuong;
            iSoLuong = 1;
        }
    }
}