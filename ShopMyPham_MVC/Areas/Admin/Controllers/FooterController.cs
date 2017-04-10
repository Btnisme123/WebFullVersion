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
    public class FooterController : Controller
    {
        // GET: Admin/Footer
        public ActionResult Index(string searchString, int page = 1, int pagesize = 10)
        {
            var footer = new CodeFooter();
            var model = footer.ListPaging(searchString, page, pagesize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Edit(string ID)
        {
            var dao = new CodeFooter();
            var model = dao.GetById(ID);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Footer model)
        {
            if (ModelState.IsValid)
            {
                var addFooter = new CodeFooter();
                string ID = addFooter.Insert(model);
                if (ID != null)
                {
                    RedirectToAction("Index", "Footer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công !");
                }

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Footer model)
        {
            if (ModelState.IsValid)
            {
                var editFooter = new CodeFooter();
                var result = editFooter.Update(model);
                if (result)
                {
                    RedirectToAction("Index", "Footer");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập không thành công !");
                }
            }
            return RedirectToAction("Index");
        }
    }
}