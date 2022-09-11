using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models;

namespace QLBanHangLuuNiem.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private QLBanhEntities db = new QLBanhEntities();
        public List<Giohang> Index()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //gio hang chưa tồn tại thì khởi tạo giỏ hàng rỗng
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
            //return View(lstGiohang);
        }
        [HttpGet]
        public ActionResult ThemGioHang(int iMaSP, string strURL, int? iMaMenu, List<Giohang> cart)
        {

            string tmp = "";
            List<Giohang> lstGiohang = Index();
            Session["Giohang"] = lstGiohang;
            Giohang sanpham = lstGiohang.FirstOrDefault(n => n.iMaSP == iMaSP);
            //Lấy thông ma menu
            
            var x = db.Menus.FirstOrDefault(n => n.MaMenu == iMaMenu);
            if (lstGiohang.Count() == 0)
            {
                //tmp = sanpham.iMaMenu.ToString();
                tmp = iMaMenu.ToString();
                if (sanpham == null)
                {
                    sanpham = new Giohang(iMaSP);
                    lstGiohang.Add(sanpham);
                    return Redirect(strURL);
                    //return Redirect("GioHang");
                }
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
            else
            //Ktr da có chưa neus  ID Menu
            {
                 var y = lstGiohang.Where(n => n.iMaMenu == iMaMenu).ToList();
                foreach(var item in y)
                {
                    tmp = item.iMaMenu.ToString();
                    break;
                }

                if (tmp == iMaMenu.ToString())
                {
                    if (sanpham == null)
                    {
                        sanpham = new Giohang(iMaSP);
                        lstGiohang.Add(sanpham);
                        return Redirect(strURL);
                        //return Redirect("GioHang");
                    }
                    sanpham.iSoLuong++;
                    return Redirect(strURL);
                }
                else
                {
                    TempData["Msg"] ="Bạn có muốn xóa giỏ hàng?";
                    lstGiohang.Clear();
                    sanpham = new Giohang(iMaSP);
                    lstGiohang.Add(sanpham);
                    return Redirect(strURL);
                }
            }
            //tra ve null
            //ktr sp
          
            //da có thi clear
            //gửi thong 
            //Q&A  CLEAR


        }


        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Index();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "NhaHang");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGiohang(int iMaSP)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            //Kiem tra san pham da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Neu ton tai thi cho sua so luong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMaSP == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "NhaHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoatatcaGiohang()
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            lstGiohang.Clear();
            return RedirectToAction("Index", "SanPham");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            //Kiem tra san pham da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Neu ton tai thi cho sua so luong
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult Dathang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "NhaHang");
            }
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection, string NgayGiao)
        {
            //Them Don Hang
            HoaDon ddh = new HoaDon();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<Giohang> gh = Index();
            ViewBag.MaHD = new SelectList
            (db.CTHDs.ToList().OrderBy(s => s.MaMenu), "MaHD", "MaMenu");
            var k = 0;
            var so = db.CTHDs.Count();
            for (int i = so; i >= 0; i++)
            {
                var ktr = db.CTHDs.FirstOrDefault(s => s.MaHD == k);
                if (ktr == null)
                {
                    foreach (var x in gh)
                    {
                        ddh.MaKH = kh.MaKH;
                        ddh.NgayDat = DateTime.Now;
                        //var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
                        //ddh.NgayGiao = DateTime.Parse(ngaygiao);
                        //if (NgayGiao.Trim() != "")
                        //    ddh.NgayGiao = Convert.ToDateTime(NgayGiao);
                        ddh.NgayGiao = DateTime.Now.AddHours(1);
                        ddh.TinhTrangGiaoHang = false;
                        ddh.DaThanhToan = false;
                        ddh.MaMenu = x.iMaMenu;
                        ddh.MaHD = k;
                        db.HoaDons.Add(ddh);
                    }
                    db.SaveChanges();
                    AddHD(k);
                    //Them chi tiet don hang

                    Session["GioHang"] = null;

                    return RedirectToAction("Xacnhandonhang", "Giohang");
                }
                k = i;

            }
            return RedirectToAction("Xacnhandonhang", "Giohang");


            //return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        //tách
        public void AddHD(int k)
        {
            List<Giohang> gh = Index();
            foreach (var item in gh)
            {
                CTHD ctdh = new CTHD();
                ctdh.MaHD = k;
                ctdh.MaSP = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.MaMenu = item.iMaMenu;
                ctdh.DonGiaBan = (decimal)item.iDonGia;
                db.CTHDs.Add(ctdh);
            }


            db.SaveChanges();
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
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
        protected void setAlert(string message,string type)
        {
            TempData["AlertMessage"] = message;
            switch (type)
            {
                case "warning":
                    TempData["AlertType"] = "warning"; break;

            }
        }

    }
}