using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.Insert(model);
                if (id > 0)
                {
                    SetAlert("Thêm thông tin thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Thêm user không thành công", "error");
                    ModelState.AddModelError("", "Thêm thông tin không thành công");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ProductDao();
            var product = dao.GetById(id);

            SetViewBag(product.CategoryID);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();

                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Update content thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật content không thành công");
                }
            }
            SetViewBag(model.CategoryID);
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}