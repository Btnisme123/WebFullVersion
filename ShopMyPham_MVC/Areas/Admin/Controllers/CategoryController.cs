using DataTables.Mvc;
using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using ShopMyPham_MVC.Common;
using System.Data.Entity.Infrastructure;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    [Author]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            try
            {
                var model = new CodeCategory();
                var result = model.ListAllCategory(0);
                return View(result);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;

                return View();
            }
        }
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var category = new CodeCategory().ViewDetail(id);
            return View(category);
        }
 
        public JsonResult DataTableGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var context = new ShopBanHangDbContext();
            var model = new CodeCategory();
            IQueryable<CategoryModel> query = model.ListAllCategory(0).AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value));
            }

            var filteredCount = query.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            orderByString = orderByString == String.Empty ? "name asc" : orderByString;
            query = query.OrderBy(orderByString);

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(p => new
            {
                Id = p.ID,
                Name = p.Name,
                CreatedDate = p.CreatedDate.ToString("dd-MM-yyyy"),
                UserName = p.UserName,
                Action = "<br /><p class=\"btn-action\"><a href=\"#\" class=\"detail\"><i class=\"ui-tooltip fa fa-pencil-square-o detail\" style=\"font-size: 22px;\" data-original-title=\"Detail\"></i></a> <a href=\"#\" class=\"remove\" ><i class=\"ui-tooltip fa fa-trash-o remove\" style=\"font-size: 20px;\" data-original-title=\"Delete\" tooltip=\"Delete\"></i></a></p>",
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult Create(Category entity)
        //{
        //    UserLogin session = (UserLogin)Session[CommonConstants.USER_SESSION];
        //    try
        //    {

        //        entity.CreatedBy = session.UserID;
        //        var model = new CodeCategory();
        //        model.Insert(entity);
        //        return RedirectToAction("Index");
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        ViewData["Error"] = ex.Message;
        //        return View(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["Error"] = ex.Message;
        //        return View(entity);
        //    }
        //}
        //[HttpPost]
        //public ActionResult Edit(Category entity)
        //{
        //    try
        //    {

        //        var model = new CodeCategory();
        //        model.Update(entity);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["Error"] = ex.Message;
        //        return View(entity);
        //    }
        //}
        [HttpPost]
        public JsonResult Create(Category category)
        {
            UserLogin session = (UserLogin)Session[CommonConstants.USER_SESSION];
            try
            {
                if (string.IsNullOrEmpty(category.Name))
                {

                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }

                category.CreatedBy = session.UserID;
                var model = new CodeCategory();
                model.Insert(category);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException ex)
            {
                ViewBag["Error"] = ex.Message;
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ViewBag["Error"] = ex.Message;
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public ActionResult Edit(Category entity)
        {
            UserLogin session = (UserLogin)Session[CommonConstants.USER_SESSION];
            try
            {
                if (string.IsNullOrEmpty(entity.Name))
                {

                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }
                //entity.CreatedBy = session.UserID;
                var model = new CodeCategory();
                model.Update(entity);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException ex)
            {
                ViewBag["Error"] = ex.Message;
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ViewBag["Error"] = ex.Message;
                return Json(new { Success = false}, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]

        public JsonResult Delete(long ID)
        {
            new CodeCategory().Delete(ID);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}