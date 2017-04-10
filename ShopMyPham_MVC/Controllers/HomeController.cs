using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Common;
using ShopMyPham_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.ServiceShopBanHang;
//using Product = ShopMyPham_MVC.ServiceShopBanHang.Product;

namespace ShopMyPham_MVC.Controllers
{
    public class HomeController : Controller
    {
      
        // GET: Home
        public ActionResult Index()
        {
            //Dung service
            List<ServiceShopBanHang.Product> listFeatureProduct = new List<ServiceShopBanHang.Product>();
            try
            {
               
            }
            catch
            {
                // ignored
            }
            finally
            {

                var model = new CodeProduct();

                ViewBag.FeatureProduct = listFeatureProduct;

                ViewBag.NewProduct = model.GetNewProduct();
            }
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new CodeMenu().ListgetById(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Banner()
        {
            var model = new CodeBanner().GetBannerHome(2);
            return PartialView(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txtUser, string txtPass)
        {
            var loginUser = new CodeLogin();
            var usercode = new CodeUser();
            var result = loginUser.LoginAccount(txtUser, Encryptor.MD5Hash(txtPass));
            if (result == 1)
            {
                var user = usercode.GetByID(txtUser.Trim());
                ViewBag.Hoten = user.Name;
                var userSession = new UserLogin();
                userSession.UserID = user.ID;
                userSession.UserName = user.UserName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                if (user.GroupID.Equals("3"))
                {
                    return Redirect("http://localhost:4945/lien-he");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


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


            return View("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }


        [HttpGet]
        public JsonResult ListName(string q)
        {
            var data = new CodeProduct().ListName(q);
            return Json(new { data = data, Status = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public ActionResult Search(string keyword, int page = 1, int pageSize = 10)
        {
            int totalRecord = 0;
            var model = new CodeProduct().ListSearchProduct(keyword, ref totalRecord, page, pageSize);
            //ViewBag.TagPost = new CodePost().ListPostCategory(id);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 4;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;


            return View(model);
        }
    }
}