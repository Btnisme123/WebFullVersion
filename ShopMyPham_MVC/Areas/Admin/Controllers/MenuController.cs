using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Common;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShopMyPham_MVC.Areas.Admin.Models;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    [Author]
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var menu = new CodeMenu();
            var model = menu.ListPaging(searchString, page, pagesize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "Add_Menu")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "Edit_Menu")]
        public ActionResult Edit(int id)
        {
            var menu = new CodeMenu().ViewDetail(id);
            SetViewBag(menu.ID);
            return View(menu);
        }

        [HttpPost]
        [HasCredential(RoleID = "Add_Menu")]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var addmenu = new CodeMenu();
                long Id = addmenu.Insert(menu);
                if (Id > 0)
                {
                    RedirectToAction("Index", "Menu");
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
        [HasCredential(RoleID = "Edit_Menu")]
        public ActionResult Edit(Menu menu)
        {
            if(ModelState.IsValid)
            {
                var editmenu = new CodeMenu();
                var result = editmenu.Update(menu);
                if(result)
                {
                    RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập không thành công !");
                }
            }
            SetViewBag(menu.ID);
            return RedirectToAction("Index");

        }
        [HttpDelete]
        [HasCredential(RoleID = "Delete_Menu")]
        public ActionResult Delete(int ID)
        {
            new CodeMenu().Delete(ID);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CodeMenuType();
            ViewBag.TypeID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
    }
}