using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShopMyPham_MVC.CodeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.Areas.Admin.Models;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    [Author]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var model = new CodeOrder();
            var result = model.ListPaging(searchString, page, pagesize);
            return View(result);
        }
        [HttpPost]
        public JsonResult ChangeStatus(long Id)
        {
            var result = new CodeOrder().ChangeStatus(Id);
            return Json(new
            {
                status = result
            });
        }
        //[HttpGet]
        //public JsonResult ViewOrderDetail(long id)
        //{
        //    var model = new CodeOrderDetail();
        //    var result = model.ListPaging(id);
        //    //string isoJson = JsonConvert.SerializeObject(result);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public ActionResult OrderDetail(long id,int page=1,int pagesize=5)
        {
            var model = new CodeOrderDetail();
            var result = model.ListPaging(id,page,pagesize);
            return View(result);
        }
    }
}