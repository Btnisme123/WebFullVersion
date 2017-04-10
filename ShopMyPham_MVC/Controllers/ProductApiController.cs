
using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Common;
using ShopMyPham_MVC.Framework;
using ShopMyPham_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace ShopMyPham_MVC.Controllers
{
    public class ProductApiController : Controller
    {
        ShopBanHangDbContext db = new ShopBanHangDbContext();
      
        [HttpGet]
        public ActionResult getInfor(long id)
        {
            List<Product> pro = db.Products.Where(n => n.ID == id && n.CategoryID == 3).ToList();
            return Json(pro, JsonRequestBehavior.AllowGet);
        }

      

        [HttpGet]
        public ActionResult getInforByAll(long id, int category)
        {
            List<Product> pro = db.Products.Where(n => n.ID == id && n.CategoryID == category).ToList();
            return Json(pro, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getInforByName(long id, int category, string name)
        {
            List<Product> pro = db.Products.Where(n => n.ID == id && n.CategoryID == category && n.Name==name).ToList();
            return Json(pro, JsonRequestBehavior.AllowGet);
        }
    }
}
