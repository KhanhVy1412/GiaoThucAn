using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models;
using QLBB.Models.ViewModels;

namespace QLBB.Controllers
{
    public class NhaHangController : Controller
    {
        private QLBanhEntities db = new QLBanhEntities();
        // GET: NhaHang
        public ActionResult Index(Menu menuList)
        {
            Session["Menu"] = menuList;
            List<Menu> dsp = db.Menus.Where(s=>s.NoiBat==true).ToList();
            Session["Menu"] = dsp;
            return View(dsp);
        }
        public ActionResult Details(int? id,int? iMaMenu,Menu menuList)
        {
            //var model = db.SanPhams.Where(s => s.MaMenu == id && s.Deleted == false).Select(i => new bigmodel
            //{
            //    TenMenu = i.Menu.TenMenu,
            //    HinhSP=i.HinhSP,
            //    TenSP=i.TenSP,
            //    GhiChu=i.GhiChu,
            //    DonGia=i.DonGia,
            //}).ToList();
                 
            //foreach (var item in model)
            //{
            //     var p = db.SanPhams.Where(s => s.MaMenu == id && s.Deleted == false).ToList();
            //}
            List<SanPham> model = db.SanPhams.Where(s => s.MaMenu == id && s.Deleted == false).ToList();
            //List<Giohang> gh = Index();
            //List<bigmodel> lstGiohang = Session["bigmodel"] as List<bigmodel>;
            return View(model);
        }
        public ActionResult List()
        {
            List<Menu> dsp = db.Menus.ToList();
            return View(dsp);
        }
        public ActionResult SearchByName(string name)
        {
            //List<SanPham> p = db.SanPhams.Where(s => s.TenSP.Contains(name)).ToList();
            List<Menu> p = db.Menus.Where(s => s.TenMenu.Contains(name)).ToList();

            ViewBag.keyword = name;

            return View(p);
        }
        public ActionResult XemCTHD(int? id)
        {

            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            var ls = from s in db.HoaDons.OrderByDescending(s=>s.NgayDat)
                     where s.MaKH == kh.MaKH && s.TinhTrangGiaoHang==false
                     select s;

            return PartialView(ls.ToList());
        }

        public ActionResult LSDH(int? id)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            var ls = from s in db.HoaDons.OrderByDescending(s => s.NgayDat)
                     where s.MaKH == kh.MaKH && s.TinhTrangGiaoHang == true
                     select s;

            return PartialView(ls.ToList());

        }
        public ActionResult XemChiTiet(int? id)
        {
            CTHD CT = db.CTHDs.FirstOrDefault(s => s.MaHD == id);
            return View(CT);
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