﻿using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID ="VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                user.Password = Encryotor.MD5Hash(user.Password);
                user.CreatedDate = DateTime.Now;
                var id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Update user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm user không thành công", "error");
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptMd5 = Encryotor.MD5Hash(user.Password);
                    user.Password = encryptMd5;
                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Update user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStauts(id);
            return Json(new
            {
                status = result
            });
        }
    }
}