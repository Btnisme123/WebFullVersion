using ShopMyPham_MVC.CodeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.Framework;

namespace ShopMyPham_MVC.Controllers
{
    public class ProductController : Controller
    {
        ShopBanHangDbContext db= new ShopBanHangDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult ProductDetail(long id)
        {
            var model = new CodeProduct();
            var result = model.ViewDetail(id);
            return View(result);
        }
        public ActionResult ProductCategory(int id, int page = 1, int pageSize = 4)
        {
            var productcategory = new CodeCategory().ViewDetail(id);
            ViewBag.Category = productcategory;
            int totalRecord = 0;
            var model = new CodeProduct().ListProductCategory(id, ref totalRecord, page, pageSize);
            //ViewBag.TagPost = new CodePost().ListPostCategory(id);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

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
        public ActionResult PostAll(int page = 1, int pageSize = 10)
        {
            int totalRecord = 0;
            var model = new CodeProduct().ListAll(ref totalRecord, page, pageSize);
          
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

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
        [ChildActionOnly]
        public ActionResult Categories()
        {
            var model = new CodeCategory().ListAll();
            return View(model);
        }

        public JsonResult Search(string keyWord)
        {
            var data = (from p in db.Products
                        where p.Name.ToLower().Contains(keyWord.ToLower())
                        select new { p.Name }).Distinct();

            return this.Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}