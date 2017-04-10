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
    public class FeedbackController : Controller
    {
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            try
            {
                var model = new CodeContact();
                var result = model.GetAllFeedback();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        public JsonResult DataTableGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var context = new ShopBanHangDbContext();
            var model = new CodeContact();
            IQueryable<FeedbackModel> query = model.GetAllFeedback().AsQueryable();

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

            var data = query.Select(a => new
            {
                ID = a.ID,
                Name = a.Name,
                Phone = a.Phone,
                Email = a.Email,
                Content = a.Content,
                UserID = a.UserID,
                CreatedDate = a.CreatedDate,
               
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
    }
}
