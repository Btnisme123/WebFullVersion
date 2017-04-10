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
    public class ShopController : Controller
    {
        ShopBanHangDbContext db = new ShopBanHangDbContext();
        // GET: Admin/Shop
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            try
            {
                var model = new CodeShop();
                var result = model.GetAllShop();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        public ActionResult Create()
        {
            GetSelectedList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Shop entity)
        {
        
            if (ModelState.IsValid)
            {

                try
                {
                    Shop shop = entity;
                    var model = new CodeShop();
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
                GetSelectedList();
                return View(entity);
            }

        }

        public JsonResult DataTableGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var context = new ShopBanHangDbContext();
            var model = new CodeShop();
            IQueryable<ShopModel> query = model.GetAllShop().AsQueryable();

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
            //squery = query.OrderBy(orderByString);

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(p => new
            {
                Id = p.ID,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                UserID = p.UserID,
                Email = p.Email,
                Action = "<br /><p class=\"btn-action\"><a href=\"#\" class=\"detail\"><i class=\"ui-tooltip fa fa-pencil-square-o detail\" style=\"font-size: 22px;\" data-original-title=\"Detail\"></i></a> <a href=\"#\" class=\"remove\" ><i class=\"ui-tooltip fa fa-trash-o remove\" style=\"font-size: 20px;\" data-original-title=\"Delete\" tooltip=\"Delete\"></i></a></p>",
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public void GetSelectedList()
        {

            var model = new CodeShop();
            List<Shop> shops = model.ListAll();
            List<Shop> filterList = shops.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
            IEnumerable<SelectListItem> userTypeList = new SelectList(filterList, "UserID", "UserID");
            ViewData["UserIDList"] = userTypeList;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Shop entity)
        {
            try
            {
                var model = new CodeShop();
                model.Update(entity);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View(entity);
            }
        }

        public ActionResult Edit(long ID)
        {
            var model = new CodeShop();
            var result = model.ViewDetail(ID);
            GetSelectedList();
            return View(result);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                var model = new CodeShop();
                model.Delete(id);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { Success = false, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
