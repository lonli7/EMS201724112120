using EMS201724112120.UserCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS201724112120.Controllers
{
    public class DefaultController : Controller
    {
        //登录验证控制器
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var name = Session["name"] as String;
            if (String.IsNullOrEmpty(name))
            {
                filterContext.Result = RedirectToAction("../Login.aspx");
                
            }
        }
    }
}