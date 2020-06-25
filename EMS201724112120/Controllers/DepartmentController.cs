using EMS201724112120.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace EMS201724112120.Controllers
{
    public class DepartmentController : DefaultController
    {
        private SystemEntities db = new SystemEntities();
        // GET: Department
        //查找所有部门
        public ActionResult Index(int? page)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            var departments = from a in db.Departments select a;
            int pageNumber = page ?? 1;
            int pageSize = 10;
            departments = departments.OrderBy(a => a.Id);
            IPagedList<Department> pageList = departments.ToPagedList(pageNumber,pageSize);
            return View(pageList);
        }
        //条件查询
        public ActionResult findBy(int? page, String word, int? num)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            var departments = from s in db.Departments where (s.name.Contains(word)) select s;
            if (num == 1)
            {
                departments = from s in db.Departments where (s.Id.ToString().Contains(word)) select s;
            }
            int pageNumber = page ?? 1;
            int pageSize = 10;
            departments = departments.OrderBy(a => a.Id);
            IPagedList<Department> pagedList = departments.ToPagedList(pageNumber, pageSize);
            return View("Index", pagedList);
        }
        //增加一个部门
        public ActionResult Add(Department department)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                db.Departments.Add(department);
                db.SaveChanges();
                String message = "success";
                return Content(message);
            }catch(Exception ex)
            {
                return Content("增加失败!" + ex.Message);
            }
        }
        //回显要修改的数据
        public ActionResult EditData(int id)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                Department department = (from a in db.Departments where a.Id == id select a).FirstOrDefault();
                return View(department);
            }catch (Exception ex)
            {
                return Content("回显数据失败" + ex.Message);
            }
        }
        //修改部门信息
        public ActionResult EditDataed(Department department)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                DbEntityEntry<Department> entry = db.Entry<Department>(department);
                entry.State = EntityState.Unchanged;
                entry.Property(a => a.Id).IsModified = true;
                entry.Property(a => a.name).IsModified = true;
                entry.Property(a => a.admin).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Department");
            }catch(Exception ex)
            {
                return Content("修改失败" + ex.Message);
            }
        }

        //删除部门信息
        public ActionResult Delete(int id)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                Department department = new Department() { Id = id };
                db.Departments.Attach(department);
                db.Departments.Remove(department);
                db.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            catch(Exception ex)
            {
                return Content("删除失败" + ex.Message);
            }
        }
        //获取当前部门下的所有员工
        public ActionResult AllEmployees(int id)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            //根据id查找当前部门
            Department department = (from a in db.Departments where a.Id == id select a).FirstOrDefault();
            //得到当前部门的所有员工集合
            ICollection<Employee> list = department.Employees;
            ViewData["datalist"] = list;
            return View();
        }
        public Boolean check()
        {
            var admin = Session["admin"] as String;
            return admin == "true" ? false : true;
        }
    }
}