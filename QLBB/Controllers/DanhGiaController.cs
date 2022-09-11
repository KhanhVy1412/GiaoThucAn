using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models;
using QLBB.Models.ViewModels;

namespace QLBB.Controllers
{
    public class DanhGiaController : Controller
    {
        private QLBanhEntities db = new QLBanhEntities();
        // GET: DanhGia
        public ActionResult Index(int ?F)
        {
            List<Comment> cmt = db.Comments.OrderByDescending(s => s.Createon).Where(s => s.MaMenu == F /*&& s.Createon.Value.Month == DateTime.Now.Month*/).ToList();
            return View(cmt);
        }
        [HttpGet]
        public ActionResult PostComment(int? F)
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostComment(object sender, string cmttext, int? F,EventArgs e)
        {
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            //F = Convert.ToInt32(Request.QueryString["imenu"]);
            Comment c = new Comment();
           c.DanhGia = cmttext;
            var querystringFromUrlReferrer = HttpUtility.ParseQueryString(HttpContext.Request.UrlReferrer.Query);
             c.MaMenu = int.Parse(querystringFromUrlReferrer["F"]);
            c.MaKH = kh.MaKH;
                c.Createon = DateTime.Now;
                db.Comments.Add(c);
                db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult XemDanhGia(int? id)
        {
            List<Comment> cmt = db.Comments.OrderByDescending(s => s.Createon).Where(s => s.MaMenu == id).ToList();
            return View(cmt);
        }
    }
}