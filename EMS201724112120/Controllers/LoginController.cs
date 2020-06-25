using EMS201724112120.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS201724112120.Controllers
{
    //登录验证控制器
    public class LoginController : Controller
    {
        private SystemEntities db = new SystemEntities();
        // GET: Login
        //登录验证及转发
        public ActionResult Index(int id,String pwd)
        {
            Employee employee = (from a in db.Employees where a.Id == id select a).FirstOrDefault();
            
            if (employee == null)
            {
                var result = new { url = "", message = "用户不存在，请重新输入", code = 202 };
                return Json(result);
            }
            if (employee.password != pwd)
            {
                var result = new { url = "", message = "密码错误，请重新输入", code = 202 };
                return Json(result);
            }
            if (employee.admin==true)
            {
                Session["name"] = employee.name;
                Session["admin"] = "true";
                var result = new { url = "/Employee", message = "登录成功，正在跳转" ,code=0};
                return Json(result);
            }
            else
            {
                Session["name"] = employee.name;
                Session["admin"] = "false";
                var result = new { url = "/Equipment/eIndex", message = "登录成功，正在跳转", code = 1 };
                return Json(result);
            }
        }
        //登出
        public ActionResult Logout()
        {
            Session["name"] = "";
            Session["admin"] = "";
            return RedirectToAction("../Login.aspx");
        }
    }
}