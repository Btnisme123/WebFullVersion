using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.Areas.Admin.Models;


namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    [Author]
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var model = new CodeAbout();
            var result = model.ListPaging(searchString, page, pagesize);
            return View(result);
        }
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            var menu = new CodeAbout().ViewDetail(id);
            return View(menu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(About about)
        {
            if (ModelState.IsValid)
            {
                var addabout = new CodeAbout();
                long Id = addabout.Insert(about);
                if (Id > 0)
                {
                    RedirectToAction("Index", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công !");
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(about);
            }


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(About about)
        {
            if (ModelState.IsValid)
            {
                var editabout = new CodeAbout();
                var result = editabout.Update(about);
                if (result)
                {
                    RedirectToAction("Index", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập không thành công !");
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(about);
            }


        }
        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            new CodeAbout().Delete(ID);
            return RedirectToAction("Index");
        }
    }
}