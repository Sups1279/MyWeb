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
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source =.; Initial Catalog = ComputerMuda; Integrated Security = True");
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }
    }
}