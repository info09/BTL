using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag();
            return View();
        }

        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetById(id);
            SetViewBag(content.CategoryID);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag(model.CategoryID);
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}