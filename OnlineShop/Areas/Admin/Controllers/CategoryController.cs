using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                long id = dao.Insert(model);
                if (id > 0)
                {
                    SetAlert("Thêm thông tin thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Thêm user không thành công", "error");
                    ModelState.AddModelError("", "Thêm thông tin không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new CategoryDao();
            var category = dao.GetById(id);
            
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();

                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Update content thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật content không thành công");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}