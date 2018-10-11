using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        // GET: Admin/OrderDetail
        public ActionResult Index(DateTime? date, int page = 1, int pageSize = 5)
        {
            var dao = new OrderDetailDao();
            var model = dao.ListOrder(date, page, pageSize);
            ViewBag.SearchString = date;
            return View(model);
        }
    }
}