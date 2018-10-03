using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new MenuDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Menu model)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                long id = dao.Insert(model);
                if (id > 0)
                {
                    SetAlert("Thêm thông tin thành công", "success");
                    return RedirectToAction("Index", "Menu");
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
        [ValidateInput(false)]
        public ActionResult Edit(long id)
        {
            var dao = new MenuDao();
            var menu = dao.GetById(id);
            return View(menu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Menu model)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();

                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Update content thành công", "success");
                    return RedirectToAction("Index", "Menu");
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
            new MenuDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}