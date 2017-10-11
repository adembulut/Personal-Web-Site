using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Web.Models;
namespace MVC_Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Admin"] == null)
                Session["Admin"] = (new dkPersonelWebContext()).Users.FirstOrDefault(x => x.isAdmin == true);
            //if (Session["Admin"] == null || Session["Admin"].ToString() != "Admin")
            //{
            //    filterContext.Result = new RedirectResult("~/Home/");
            //}

            //base.OnActionExecuting(filterContext);
        }
    }
}