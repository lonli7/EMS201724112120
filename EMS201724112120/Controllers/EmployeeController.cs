using EMS201724112120.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Configuration;

namespace EMS201724112120.Controllers
{
    public class EmployeeController : DefaultController
    {
        private SystemEntities db = new SystemEntities();
       
        
        //主页进行查询员工操作
        public ActionResult Index(int? page)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            var employees = from s in db.Employees select s;
            int pageNumber = page ?? 1;
            int pageSize = 10 ;
            employees = employees.OrderBy(a => a.Id);
            IPagedList<Employee> pagedList = employees.ToPagedList(pageNumber, pageSize);
            
            return View(pagedList);
        }
        //条件查询
        public ActionResult findBy(int? page,String word,int? num)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            var employees =  from s in db.Employees where (s.name.Contains(word)) select s ;
            if (num == 1)
            {
                employees = from s in db.Employees where (s.Id.ToString().Contains(word)) select s;
            }
            int pageNumber = page ?? 1;
            int pageSize = 10;
            employees = employees.OrderBy(a => a.Id);
            IPagedList<Employee> pagedList = employees.ToPagedList(pageNumber, pageSize);
            return View("Index",pagedList);
        }
        //执行增加操作
        public ActionResult Add(Employee employee)
        {
            try
            {
                if (check())
                {
                    return RedirectToAction("eIndex", "Equipment");
                }
                db.Employees.Add(employee);
                db.SaveChanges();
                String message = "success";
                return Content(message);
            }
            catch(Exception ex)
            {
                return Content("增加失败！" + ex.Message);
            }
            
            
        }
        //执行删除操作
        public ActionResult Delete(int id)
        {
            try
            {
                if (check())
                {
                    return RedirectToAction("eIndex", "Equipment");
                }
                Employee employee = new Employee() { Id = id };
                db.Employees.Attach(employee);   //通过主键实例化的对象添加到EF管理容器中
                db.Employees.Remove(employee);   //将对象状态标记为删除状态
                db.SaveChanges();                //操作更新到数据库
                return RedirectToAction("Index", "Employee");
            }
            catch(Exception ex)
            {
                return Content("删除失败！"+ex.Message);
            }
            
        }

        //显示要修改的数据
        public ActionResult EditData(int id)
        {
            try
            {
                if (check())
                {
                    return RedirectToAction("eIndex", "Equipment");
                }
                Employee employee = (from a in db.Employees where a.Id ==id select a).FirstOrDefault();
                IEnumerable<SelectListItem> listItem = (from c in db.Departments select c).ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.name });
                ViewBag.CateList = listItem;
                return View(employee);
            }catch(Exception ex)
            {
                return Content("回显修改信息失败" + ex.Message);
            }
        }

        //执行修改操作
        public ActionResult EditDataed(Employee employee)
        {
            try
            {
                if (check())
                {
                    return RedirectToAction("eIndex", "Equipment");
                }
                DbEntityEntry<Employee> entry = db.Entry<Employee>(employee);
                entry.State = EntityState.Unchanged;
                entry.Property(a => a.name).IsModified = true;
                entry.Property(a => a.password).IsModified = true;
                entry.Property(a => a.sex).IsModified = true;
                entry.Property(a => a.phone).IsModified = true;
                entry.Property(a => a.admin).IsModified = true;
                entry.Property(a => a.departmentId).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            catch(Exception ex)
            {
                return Content("修改失败" + ex.Message);
            }
        }
        public Boolean check()
        {
            var admin = Session["admin"] as String;
            return admin=="true"?false:true;
        }
        
        
    }
}