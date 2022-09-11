using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models;
using QLBB.Models.ViewModels;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using PagedList;
using PagedList.Mvc;
namespace QLBB.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        // GET: Admin/SanPham
        private QLBanhEntities db = new QLBanhEntities();
        // GET: Phim
        public ActionResult Home()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            DoiTac dt = Session["TaiKhoan"] as DoiTac;
            var listsp = from s in db.SanPhams
                         where s.MaMenu == dt.MaMenu && !s.Deleted
                         select s;

            return PartialView(listsp.ToList());
        }
        public ActionResult Index()
        {

            /*List<Phim> dsp = db.Phims.ToList();*/
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            DoiTac dt = Session["TaiKhoan"] as DoiTac;
            var listsp = from s in db.SanPhams
                         where s.MaMenu == dt.MaMenu && !s.Deleted
                         select s;
            
            return PartialView(listsp.ToList());
        
            

        }
        public ActionResult Create()
        {
            //Lấy ra ds loại sp
            SanPhamViewModels pModel = new SanPhamViewModels();
            pModel.ListPhanLoai = db.PhanLoais.ToList();
            pModel.ListMenu = db.Menus.ToList();
            return View(pModel);
        }
        [HttpPost]
        public ActionResult Create(SanPham p, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            SanPham SanPhamM = new SanPham();
            SanPhamM.TenSP = p.TenSP;
            SanPhamM.DonGia = p.DonGia;
            SanPhamM.MaPhanLoai = p.MaPhanLoai;
            SanPhamM.MaMenu = p.MaMenu;
            SanPhamM.GhiChu = p.GhiChu;
            SanPhamM.Deleted = false;
            //lưu đường dẫn vào database
            SanPhamM.HinhSP = "Content/image/" + fileName;
            //sanPhamMoi.HinhSP = sp.HinhSP;
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            DoiTac kh = Session["TaiKhoan"] as DoiTac;
            SanPhamM.MaMenu = kh.MaMenu;
            db.SanPhams.Add(SanPhamM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)//truyền id
        {
            SanPham p = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            ViewBag.MaPhanLoai = new SelectList
                (db.PhanLoais.ToList().
                OrderBy(s => s.TenPhanLoai), "MaPhanLoai", "TenPhanLoai");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            SanPham p = db.SanPhams.Where(s => s.MaSP == sanpham.MaSP).FirstOrDefault();
            p.TenSP = sanpham.TenSP;
            p.DonGia = sanpham.DonGia;
            p.GhiChu = sanpham.GhiChu;
            p.MaPhanLoai = sanpham.MaPhanLoai;
            p.HinhSP = "Content/image/" + fileName; ;

            /* db.SanPhams.Add(sp);*///Thêm sp
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            SanPham p = db.SanPhams.FirstOrDefault(s => s.MaSP == id);
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            SanPham p = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            ViewBag.MaLoaiPhim = new SelectList
               (db.PhanLoais.ToList().
                OrderBy(s => s.TenPhanLoai), "MaPhanLoai", "TenPhanLoai");
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(SanPham sanpham, int id)
        {
            SanPham p = db.SanPhams.Single(s => s.MaSP == id);
            p.Deleted = true;
            //db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult SearchByName(string name)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.TenSP.Contains(name)&& !s.Deleted).ToList();

            ViewBag.keyword = name;

            return View(p);
        }
        public ActionResult XemCTHD(int? id)
        {
            //int pageSize = 6;
            //int pageNum = (page ?? 1);
            //var cthd = (from l in db.HoaDons
            //            select l).OrderByDescending(s => s.MaHD);

            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            DoiTac kh = Session["TaiKhoan"] as DoiTac;
            var ls = from s in db.HoaDons.OrderByDescending(s => s.NgayDat)
                     where s.MaMenu == kh.MaMenu &&s.TinhTrangGiaoHang == false
                     select s;
            //ViewBag.MaHD = new SelectList
            // (db.CTHDs.ToList().
            //  OrderBy(s => s.MaMenu), "MaHD", "MaMenu");
            return PartialView(ls.ToList());
            //if (db.HoaDons.FirstOrDefault(s => s.DaThanhToan.Equals())
            //Console.Write("Chưa thanh toán");

            //return View(cthd.ToPagedList(pageNum, pageSize));
        }
        //public ActionResult DonHangMoi(int? id)
        //{

        //    if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

        //        return null;
        //    DoiTac kh = Session["TaiKhoan"] as DoiTac;
        //    var ls = from s in db.HoaDons
        //             where s.MaMenu == kh.MaMenu && s.TinhTrangGiaoHang == false
        //             select s;
        //    return PartialView(ls.ToList());
        //}

       
        public double TongSoLuongDon()
        {
            DoiTac dt = Session["TaiKhoan"] as DoiTac;
            var listsp = from s in db.HoaDons
                         where s.MaMenu == dt.MaMenu && s.TinhTrangGiaoHang==false
                         select s;
            double slDH = 0;
            foreach (var item in listsp)
            {
                slDH = db.CTHDs.Count();
            }
            return slDH;
        }
        public ActionResult DonHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuongDon();
            //ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult CTHD(int id)
        {
            CTHD CT = db.CTHDs.FirstOrDefault(s => s.MaHD == id);
            return View(CT);
        }
        public ActionResult SuaHD(int id)
        {
            HoaDon p = db.HoaDons.Where(s => s.MaHD == id).FirstOrDefault();
            ViewBag.TinhTrangGiaoHang = new SelectList
                (db.GiaoHangs.ToList().OrderBy(s => s.TinhTrangGiaoHang), "TinhTrangGiaoHang", "GiaoHangTT");
            ViewBag.DaThanhToan = new SelectList
              (db.ThanhToans.ToList().OrderBy(s => s.DaThanhToan), "DaThanhToan", "TinhTrang");
            return View(p);
        }
        [HttpPost]
        public ActionResult SuaHD(HoaDon Hd)
        {
            HoaDon p = db.HoaDons.Where(s => s.MaHD == Hd.MaHD).FirstOrDefault();
            p.DaThanhToan = Hd.DaThanhToan;
            p.TinhTrangGiaoHang = Hd.TinhTrangGiaoHang;
            //p.NgayDat = Hd.NgayDat;
            //p.NgayGiao = Hd.NgayGiao;
            db.SaveChanges();
            return RedirectToAction("XemCTHD");
        }
        public ActionResult LSDH(int? id)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")

                return null;
            DoiTac kh = Session["TaiKhoan"] as DoiTac;
            var ls = from s in db.HoaDons.OrderByDescending(s => s.NgayDat)
                     where s.MaMenu == kh.MaMenu && s.TinhTrangGiaoHang == true
                     select s;
    
            return PartialView(ls.ToList());

        }
        public ActionResult DanhGia(int? id)
        {
            DoiTac dt = Session["TaiKhoan"] as DoiTac;
            var listsp = from s in db.Comments.OrderByDescending(s=>s.Createon)
                         where s.MaMenu == dt.MaMenu
                         select s;
        
            return View(listsp.ToList());
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
