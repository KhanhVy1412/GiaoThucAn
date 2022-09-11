using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models;

namespace QLBB.Areas.Admin.Controllers
{
   public class ThongKeController : BaseController
    {
        private QLBanhEntities db = new QLBanhEntities();
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//số lượng người truy cập từ application
            ViewBag.TongDoanhThu = ThongKeDoanhThuTheoQuan();
            ViewBag.ThongKeDonHang = ThongKeDonHang();
            return View();
        }
        public decimal ThongKeDoanhThu()
        { 
          
            decimal TongDoanhThu = decimal.Parse(db.CTHDs.Sum(n => n.DonGiaBan * n.SoLuong).ToString());
            return TongDoanhThu;
        }
        public decimal ThongKeDoanhThuTheoQuan()
        {
            DoiTac dt = Session["TaiKhoan"] as DoiTac;
            var listsp = from s in db.HoaDons
                         where s.MaMenu == dt.MaMenu
                         select s;
            decimal TongTien = 0;
            foreach(var item in listsp)
            {
                TongTien += decimal.Parse(item.CTHDs.Sum(n => n.DonGiaBan * n.SoLuong).Value.ToString());
            }
            return TongTien;
        }
        public double ThongKeDonHang()
        {
            DoiTac dt = Session["TaiKhoan"] as DoiTac;
            var listsp = from s in db.HoaDons
                         where s.MaMenu == dt.MaMenu
                         select s;
            double slDH = 0;
            foreach (var item in listsp)
            {
              slDH = db.CTHDs.Count();
            }
            return slDH;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
