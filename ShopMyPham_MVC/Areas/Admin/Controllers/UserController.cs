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
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var user = new CodeUser();
            var model = user.ListPaging(searchString, page, pagesize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "Add_User")]
        public ActionResult Create()
        {

            SetViewGroup(null);
            return View();
        }
        [HttpGet]
        [HasCredential(RoleID = "Edit_User")]
        public ActionResult Edit(int id)
        {
            var user = new CodeUser().ViewDetail(id);
            SetViewGroup(user.GroupID);
            return View(user);
        }
       
        [HttpPost]
        [HasCredential(RoleID = "Add_User")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var addUser = new CodeUser();
                var Mahoapass = Encryptor.MD5Hash(user.Password);
                user.Password = Mahoapass;
                user.CreatedDate = DateTime.Now;
                user.Status = true;
                long Id = addUser.Insert(user);
                if (Id > 0)
                {
                    RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới User không thành công !");
                }
                return RedirectToAction("Index");
            }
            else
            {
                SetViewGroup(null);
                return View(user);
            }

        }
        [HttpPost]
        [HasCredential(RoleID = "Edit_User")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var edituser = new CodeUser();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var mahoapass = Encryptor.MD5Hash(user.Password);
                    user.Password = mahoapass;
                }
                var result = edituser.Update(user);
                if (result)
                {
                    RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập User không thành công !");
                }
                return RedirectToAction("Index");

            }

            else
            {
                    SetViewGroup(user.GroupID);
                return View(user);
            }

        }
        [HttpDelete]
        [HasCredential(RoleID = "Delete_User")]
        public ActionResult Delete(int ID)
        {
            new CodeUser().Delete(ID);
            return RedirectToAction("Index");
        }
        [HasCredential(RoleID = "Change_Status")]
        public JsonResult ChangeStatus(long Id)
        {
            var result = new CodeUser().ChangeStatus(Id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewGroup(string GroupID)
        {
            if (string.IsNullOrEmpty(GroupID))
            {
                var dao = new CodeGroupUser();
                ViewBag.GroupID = new SelectList(dao.ListAllGroup(), "ID", "Name", GroupID);
            }

        }

    }
}