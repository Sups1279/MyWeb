using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom4_LTWeb.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Nhom4_LTWeb.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        DbMyWebDataContext db = new DbMyWebDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLSP", "TenLoaiSP");
            ViewBag.MaHang = new SelectList(db.HANGs.ToList().OrderBy(n => n.TenHang), "MaHang", "TenHang");

            return View();
        }

        [HttpPost]
        public ActionResult Create( FormCollection f, HttpPostedFileBase fl)
        {
            SANPHAM sp = new SANPHAM();
            ViewBag.MaLSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLSP", "TenLoaiSP");
            ViewBag.MaHang = new SelectList(db.HANGs.ToList().OrderBy(n => n.TenHang), "MaHang", "TenHang");
            if (fl == null)
            {
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MaLSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLSP", "TenLoaiSP");
                ViewBag.MaHang = new SelectList(db.HANGs.ToList().OrderBy(n => n.TenHang), "MaHang", "TenHang");
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    
                    var sFileName = Path.GetFileName(fl.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinh"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fl.SaveAs(path);

                    }
                    sp.TenSP = f["sTenSP"];
                    sp.MoTa = f["sMoTa"];
                    sp.HinhAnh = sFileName;
                    sp.SoLuong = int.Parse(f["iSoLuong"]);
                    sp.GiaSP = double.Parse(f["dgiaSP"]);
                    sp.MaLSP = int.Parse(f["MaLSP"]);
                    sp.MaHang = int.Parse(f["MaHang"]);
                    db.SANPHAMs.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(s => s.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteComfirm(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
            }
            var ctdh = db.CHITIETDATHANGs.Where(n => n.MaSP == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sản phẩm này này đang có trong bẩng chi tiết đặt hàng <br>" + " Nếu muốn xóa thì xóa hêt smax sản phẩm trong bảng chi tiết đơn hàng";
                return View(sp);
            }
            db.SANPHAMs.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLSP", "TenLoaiSP");
            ViewBag.MaHang = new SelectList(db.HANGs.ToList().OrderBy(n => n.TenHang), "MaHang", "TenHang");

            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));
            ViewBag.MaLSP = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoaiSP), "MaLSP", "TenLoaiSP");
            ViewBag.MaHang = new SelectList(db.HANGs.ToList().OrderBy(n => n.TenHang), "MaHang", "TenHang");

            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinh"), sFileName);

                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.HinhAnh = sFileName;
                }
                sp.TenSP = f["sTenSP"];
                sp.MoTa = f["sMoTa"];
                sp.SoLuong = int.Parse(f["iSoLuong"]);
                sp.GiaSP = double.Parse(f["dgiaSP"]);
                sp.MaLSP = int.Parse(f["MaLSP"]);
                sp.MaHang = int.Parse(f["MaHang"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sp);

        }
    }
} 