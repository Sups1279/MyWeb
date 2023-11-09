using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_LTWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=LAPTOP-VC5IF5QK\\SQLEXPRESS;Initial Catalog=ComputerMuda;Integrated Security=True");

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {

            var sHoTen = collection["HoTen"];
            var sUserName = collection["UserName"];
            var sPsw = collection["Psw"];
            var sEmail = collection["Email"];
            var sSDT = collection["SDT"];
            var sNgayDK = DateTime.Now;
            if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err1"] = "Họ tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(sUserName))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if (String.IsNullOrEmpty(sPsw))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err4"] = "Email không được rỗng";
            }
            else if (String.IsNullOrEmpty(sSDT))
            {
                ViewData["err5"] = "Số điện thoại không được rỗng";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.UserName == sUserName) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Enail đã được sử dụng";
            }
            else
            {
                kh.HoTen = sHoTen;
                kh.UserName = sUserName;
                kh.Psw = sPsw;
                kh.Email = sEmail;
                kh.SDT = sSDT;
                kh.NgayDK = sNgayDK;
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sUserName = collection["UserName"];
            var sPsw = collection["Psw"];
            if (String.IsNullOrEmpty(sUserName))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
                return View();
            }
            else if (String.IsNullOrEmpty(sPsw))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
                return View();
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.UserName == sUserName && n.Psw == sPsw);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["UserName"] = kh;
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
            }

            return RedirectToAction("Index", "Shop");
        }
        public ActionResult DangXuat()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index", "Shop");
        }

        
        public ActionResult ThongTin(int? id)
        {
            var thanhvien = from t in db.KHACHHANGs where t.MaTK == id select t;
            return View(thanhvien.SingleOrDefault());
        }
    }
}