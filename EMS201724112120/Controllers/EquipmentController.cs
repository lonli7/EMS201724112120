using EMS201724112120.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace EMS201724112120.Controllers
{
    public class EquipmentController : DefaultController
    {
        private SystemEntities db = new SystemEntities();
        // GET: Equipment
        //查询所有设备信息
        public ActionResult Index(int? page)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            var equipments = from a in db.Equipments select a;
            int pageNumber = page ?? 1;
            int pageSize = 3;
            equipments = equipments.OrderBy(a => a.Id);
            IPagedList<Equipment> pagedList = equipments.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }
        //条件查询
        public ActionResult findBy(int? page, String word, int? num)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            var equipments = from s in db.Equipments where (s.name.Contains(word)) select s;
            if (num == 1)
            {
                equipments = from s in db.Equipments where (s.Id.ToString().Contains(word)) select s;
            }
            int pageNumber = page ?? 1;
            int pageSize = 10;
            equipments = equipments.OrderBy(a => a.Id);
            IPagedList<Equipment> pagedList = equipments.ToPagedList(pageNumber, pageSize);
            return View("Index", pagedList);
        }

        //增加设备
        public ActionResult Add(Equipment equipment,String imageBase)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(imageBase);
                equipment.image = bytes;
                db.Equipments.Add(equipment);
                db.SaveChanges();
                String message = "success";
                return Content(message);
            }catch(Exception ex)
            {
                return Content("增加失败,错误信息：" + ex.Message);
            }
        }
        //删除设备
        public ActionResult Delete(int id)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                Equipment equipment = new Equipment() { Id = id };
                db.Equipments.Attach(equipment);
                db.Equipments.Remove(equipment);
                db.SaveChanges();
                return RedirectToAction("Index", "Equipment");
            }
            catch (Exception ex)
            {
                return Content("删除失败,错误信息：" + ex.Message);
            }
        }
        //回显修改设备信息
        public ActionResult EditData(int id)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                Equipment equipment = (from a in db.Equipments where a.Id == id select a).FirstOrDefault();
                return View(equipment);
            }catch(Exception ex)
            {
                return Content("回显修改数据信息失败,错误信息：" + ex.Message);
            }
        }
        //执行修改操作
        public ActionResult EditDataed(Equipment equipment)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                DbEntityEntry<Equipment> entry = db.Entry<Equipment>(equipment);
                entry.State = EntityState.Unchanged;
                entry.Property(a => a.name).IsModified = true;    //名字
                entry.Property(a => a.specs).IsModified = true;   //规格
                entry.Property(a => a.price).IsModified = true;   //价格
                entry.Property(a => a.datatime).IsModified = true;//录入时间
                entry.Property(a => a.location).IsModified = true;//产地
                entry.Property(a => a.manager).IsModified = true; //设备管理员
                db.SaveChanges();
                return RedirectToAction("Index", "Equipment");
            }
            catch (Exception ex)
            {
                return Content("修改数据失败,错误信息：" + ex.Message);
            }
        }
        //更新图片操作
        public ActionResult updateImage(int id,String imageBase)
        {
            if (check())
            {
                return RedirectToAction("eIndex", "Equipment");
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(imageBase);
                Equipment equipments = db.Equipments.Find(id);
                db.Entry(equipments).State = EntityState.Unchanged;
                equipments.image = bytes;
                db.Entry(equipments).Property("image").IsModified = true;
                db.SaveChanges();
                String message = "success";
                return Content(message);
            }catch(Exception ex)
            {
                return Content("更新图片失败,错误信息："+ex.Message);
            }
            

        }
        //非管理员主页
        public ActionResult eIndex(int? page)
        {
            var equipments = from a in db.Equipments select a;
            int pageNumber = page ?? 1;
            int pageSize = 3;
            equipments = equipments.OrderBy(a => a.Id);
            IPagedList<Equipment> pagedList = equipments.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }
        //条件查询
        public ActionResult findBye(int? page, String word, int? num)
        {
            var equipments = from s in db.Equipments where (s.name.Contains(word)) select s;
            if (num == 1)
            {
                equipments = from s in db.Equipments where (s.Id.ToString().Contains(word)) select s;
            }
            int pageNumber = page ?? 1;
            int pageSize = 10;
            equipments = equipments.OrderBy(a => a.Id);
            IPagedList<Equipment> pagedList = equipments.ToPagedList(pageNumber, pageSize);
            return View("eIndex", pagedList);
        }

        public Boolean check()
        {
            var admin = Session["admin"] as String;
            return admin == "true" ? false : true;
        }
    }
}