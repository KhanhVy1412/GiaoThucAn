//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using QLBB.Models.ViewModels;
//using System.Web.Routing;

//namespace QLBB.Areas.Admin.Controllers
//{
//    public class BaseController : Controller
//    {
//        protected override void OnActionExecuting(ActionExecutingContext filterContext)
//        {

//            if (Session["USER_SESSION"] == null && Session["SESSION_GROUP"] == null)

//            {
//                filterContext.Result = new RedirectToRouteResult(new
//                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
//            }
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBB.Models.ViewModels;
using System.Web.Routing;

namespace QLBB.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["Taikhoan"] == null )

            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "DangNhap", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}