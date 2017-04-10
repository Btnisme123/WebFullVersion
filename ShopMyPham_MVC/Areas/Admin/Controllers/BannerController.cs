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
    public class BannerController : Controller
    {
        // GET: Admin/Banner
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var banner = new CodeBanner();
            var model = banner.ListPaging(searchString, page, pagesize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
    
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var banner = new CodeBanner().ViewDetail(id);
            SetViewBag(banner.TypeID);
            return View(banner);
        }

        [HttpPost]
        public ActionResult Create(Banner menu)
        {
            if (ModelState.IsValid)
            {
                var addbanner = new CodeBanner();
                int Id = addbanner.Insert(menu);
                if (Id > 0)
                {
                    RedirectToAction("Index", "Banner");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công !");
                }
            }
            SetViewBag();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                var editbanner = new CodeBanner();
                var result = editbanner.Update(banner);
                if (result)
                {
                    RedirectToAction("Index", "Banner");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập không thành công !");
                }
            }
            SetViewBag(banner.TypeID);
            return RedirectToAction("Index");

        }
        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            new CodeBanner().Delete(ID);
            return RedirectToAction("Index");
        }
        public void SetViewBag(int? selectedID = null)
        {
            var dao = new CodeBanner();
            ViewBag.TypeID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
    }
}