using ShopMyPham_MVC.CodeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DataTables.Mvc;
using ShopMyPham_MVC.Framework;
using ShopMyPham_MVC.Areas.Admin.Models;
using System.Data.Entity.Infrastructure;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    [Author]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page=1, int pagesize=5)
        {
            try
            {
                var model = new CodeProduct();
                var result = model.GetAllProduct();
                return View(result);
            }
            catch(Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }


        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public ActionResult Edit(long ID)
        {
            var model = new CodeProduct();
            var result = model.ViewDetail(ID);
            SetViewBag(result.ID);
            return View(result);
        }
        public JsonResult DataTableGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var context = new ShopBanHangDbContext();
            var model = new CodeProduct();
            IQueryable<ProductModel> query = model.GetAllProduct().AsQueryable();

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

            orderByString = orderByString == String.Empty ? "ID asc" : orderByString;
            query = query.OrderBy(orderByString);

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(p => new
            {
                Id = p.ID,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                Category = p.Category,
                CreatedDate = p.CreatedDate.ToString("dd-MM-yyyy"),
                Action = "<br /><p class=\"btn-action\"><a href=\"#\" class=\"detail\"><i class=\"ui-tooltip fa fa-pencil-square-o detail\" style=\"font-size: 22px;\" data-original-title=\"Detail\"></i></a> <a href=\"#\" class=\"remove\" ><i class=\"ui-tooltip fa fa-trash-o remove\" style=\"font-size: 20px;\" data-original-title=\"Delete\" tooltip=\"Delete\"></i></a></p>",
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        public void SetViewBag(long ? ID= null)
        {
            //var model = new CodeCategory();          
            //ViewBag.CategoryID = new SelectList(model.ListAll(), "ID", "Name", ID);

            var model = new CodeCategory();       
            IEnumerable<SelectListItem> userTypeList = new SelectList(model.ListAll(), "ID", "ID");
            ViewData["CategoryList"] = userTypeList;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product entity)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    entity.CreatedDate = DateTime.Now;
                    entity.Status = true;
                    var model = new CodeProduct();
                    model.Add(entity);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ViewData["Error"] = ex.Message;
                    return View(entity);
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = ex.Message;
                    return View(entity);
                }
            }
            else
            {
                SetViewBag();
                return View(entity);
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product entity)
        {
            try
            {
                entity.ModifiedDate = DateTime.Now;
                var model = new CodeProduct();

                model.Update(entity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View(entity);
            }


        }
        [HttpPost]
        public JsonResult Delete(long Id)
        {
            try
            {
                var model = new CodeProduct();
                model.Delete(Id);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}