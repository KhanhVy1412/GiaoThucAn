using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models;
using QLBB.Models.ViewModels;

namespace QLBB.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        QLBanhEntities db = new QLBanhEntities();
        //    public ActionResult Index()
        //    {
        //        if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
        //        {
        //            ViewBag.username = Request.Cookies["username"].Value;
        //            ViewBag.password = Request.Cookies["password"].Value;
        //        }
        //        return View();
        //    }

        //    public void ghinhotaikhoan(string username, string password)
        //    {
        //        HttpCookie us = new HttpCookie("username");
        //        HttpCookie pas = new HttpCookie("password");

        //        us.Value = username;
        //        pas.Value = password;

        //        us.Expires = DateTime.Now.AddDays(1);
        //        pas.Expires = DateTime.Now.AddDays(1);
        //        Response.Cookies.Add(us);
        //        Response.Cookies.Add(pas);

        //    }

        //    [HttpPost]
        //    public ActionResult kiemtradangnhap(string username, string password, string ghinho)
        //    {
        //        if (Request.Cookies["username"] != null && Request.Cookies["username"] != null)
        //        {
        //            username = Request.Cookies["username"].Value;
        //            password = Request.Cookies["password"].Value;
        //        }

        //        if (checkpassword(username, password))
        //        {
        //            var userSession = new UserLogin();
        //            userSession.TenDangNhap = username;

        //            var listGroups = GetListGroupID(username);//Có thể viết dòng lệnh lấy các GroupID từ CSDL, ví dụ gán ="ADMIN", dùng List<string>

        //            Session.Add("SESSION_GROUP", listGroups);
        //            Session.Add("USER_SESSION", userSession);

        //            if (ghinho == "on")//Ghi nhớ
        //                ghinhotaikhoan(username, password);
        //            return Redirect("~/Admin/SanPham");

        //        }
        //        return Redirect("~/Admin/Login");
        //    }
        //    public List<string> GetListGroupID(string userName)
        //    {
        //        // var user = db.User.Single(x => x.UserName == userName);

        //        var data = (from a in db.Quyens
        //                    join b in db.TaiKhoans on a.MaQuyen equals b.MaQuyen
        //                    where b.TenDangNhap == userName

        //                    select new
        //                    {
        //                        UserGroupID = b.MaQuyen,
        //                        UserGroupName = a.TenQuyen
        //                    });


        //        return data.Select(x => x.UserGroupName).ToList();

        //    }
        //    public bool checkpassword(string username, string password)
        //    {
        //        if (db.TaiKhoans.Where(x => x.TenDangNhap == username && x.MatKhau == password).Count() > 0)

        //            return true;
        //        else
        //            return false;


        //    }

        //    public ActionResult SignOut()
        //    {

        //        Session["USER_SESSION"] = null;
        //        Session["SESSION_GROUP"] = null;


        //        if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
        //        {
        //            HttpCookie us = Request.Cookies["username"];
        //            HttpCookie ps = Request.Cookies["password"];

        //            ps.Expires = DateTime.Now.AddDays(-1);
        //            us.Expires = DateTime.Now.AddDays(-1);
        //            Response.Cookies.Add(us);
        //            Response.Cookies.Add(ps);
        //        }

        //        //return Redirect("/Admin/Login");
        //        return Redirect("~/SPClient/Index");
        //    }
        //}
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collectionn)
        {
            var tendn = collectionn["TenDangNhap"];
            var matkhau = collectionn["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                DoiTac kh = db.DoiTacs.SingleOrDefault(n => n.TenQuan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    Session["MaId"] = kh.MaDoiTac;
                    return Redirect("~/Admin/SanPham/Home");
                    //return RedirectToAction("GioHang", "GioHang");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult SignOut()
        {
            Session.Clear();
           
            return Redirect("~/Admin/SanPham");
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

