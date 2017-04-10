using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.Framework;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{

    public class LoginController : Controller
    {
        ShopBanHangDbContext db= new ShopBanHangDbContext();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginUser = new CodeLogin();
                var usercode = new CodeUser();
                var result = loginUser.Login(model.UserName, model.Password, true);
                if (result == 1)
                {
                    var user = usercode.GetByID(model.UserName);
                    ViewBag.Hoten = user.Name;
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.UserName = user.UserName;
                    userSession.GroupID = user.GroupID;
         
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    List<string> listRole = db.Database.SqlQuery<string>("exec GetPermissionByUserId " + user.ID.ToString()).ToList();
                    Session["strRole"] = listRole;
                    if (user.GroupID == "1") return RedirectToAction("PermissionGroup", "Permission");
                    else if (user.GroupID == "2") return RedirectToAction("Index", "Product");
                    else if (user.GroupID == "3") return Redirect("http://localhost:4945/lien-he");
                    else return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại !");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa !");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng !");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Bạn không có quyền truy cập !");
                }
            }

            return View("Index");

        }
    }
}