using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.user = new UserDao().countUser();
            ViewBag.product = new ProductDao().countProduct();
            ViewBag.content = new ContentDao().countContent();
            return View();
        }

      
    }
}