using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new SlideDao();
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
        public ActionResult Create(Slide model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var dao = new SlideDao();
                long id = dao.Insert(model);
                if (id > 0)
                {
                    SetAlert("Thêm thông tin thành công", "success");
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    SetAlert("Thêm slide không thành công", "error");
                    ModelState.AddModelError("", "Thêm thông tin không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Edit(long id)
        {
            var dao = new SlideDao();
            var slide = dao.GetById(id);
            return View(slide);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Slide model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                var dao = new SlideDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Update slide thành công", "success");
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật slide không thành công");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}