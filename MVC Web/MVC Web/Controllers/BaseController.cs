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
            {
                filterContext.Result = new RedirectResult("~/Login/");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}