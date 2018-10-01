using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page=1, int pageSize=6)
        {
            ViewBag.ProductCategory = new ProductCategoryDao().ListAll();
            int totalRecord = 0;
            var model = new ProductDao().ListAllProduct(ref totalRecord, page, pageSize);
            ViewBag.TotalPage = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        //[ChildActionOnly]
        //public PartialViewResult ProductCategory()
        //{
        //    var model = new ProductCategoryDao().ListAll();
        //    return PartialView(model);
        //}

        public ActionResult Category(long id,int page= 1, int pageSize = 3)
        {
            var category = new ProductCategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            ViewBag.ProductCategory = new ProductCategoryDao().ListAll();
            int totalRecord = 0;
            var model = new ProductDao().ListProductByCategory(id,ref totalRecord,page,pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)totalRecord / pageSize); //Tính tổng số trang

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelateProduct = new ProductDao().ListRelateProduct(id,3);
            ViewBag.ProductCategory = new ProductCategoryDao().ListAll();
            return View(product);
        }
    }
}