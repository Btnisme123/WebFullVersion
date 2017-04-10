using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.Areas.Admin.Models;
using DataTables.Mvc;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    public class BeaconController : Controller
    {

        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            try
            {
                var model = new CodeBeacon();
                var result = model.GetAllBeacon();
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
            return View();
        }

        public JsonResult DataTableGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var context = new ShopBanHangDbContext();
            var model = new CodeBeacon();
            IQueryable<BeaconModel> query = model.GetAllBeacon().AsQueryable();

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

            orderByString = orderByString == String.Empty ? "MACID asc" : orderByString;
           // query = query.OrderBy(orderByString);

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(p => new
            {
                MACID = p.MACID,
                LocationX = p.LocationX,
                LocationY = p.LocationY,
                ShopID = p.ShopID,
                Information = p.Information,
                Name = p.Name,
                Action = "<br /><p class=\"btn-action\"><a href=\"#\" class=\"detail\"><i class=\"ui-tooltip fa fa-pencil-square-o detail\" style=\"font-size: 22px;\" data-original-title=\"Detail\"></i></a> <a href=\"#\" class=\"remove\" ><i class=\"ui-tooltip fa fa-trash-o remove\" style=\"font-size: 20px;\" data-original-title=\"Delete\" tooltip=\"Delete\"></i></a></p>",
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Beacon entity)
        {
            try
            {
                var model = new CodeBeacon();
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
        public ActionResult SaveInfoBeacon(FormCollection form)
         {		
 		
        		
             Beacon beacon = new Beacon();		
             Random random = new Random();		           
             beacon.ShopID= random.Next(1, 1000);
            //beacon.LocationX = Convert.ToInt64(form.Get("txtCordinateX"));		
            //beacon.LocationY = Convert.ToInt64(form.Get("txtCordinateY"));		
            //		

            //String a = Request["LocX"].ToString();		
            //String b = Request["LocY"].ToString();		

            try
            {
                if (form.Get("txtCordinateX") != null && form.Get("txtCordinateY") != null)
                {
                    beacon.MACID = form.Get("txtMacID").ToString();
                    beacon.Information = form.Get("txtInformation").ToString();
                    beacon.ShopID = long.Parse(form.Get("txtShopID").ToString());
                    beacon.LocationX = Convert.ToDecimal(form.Get("txtCordinateX").ToString());
                    beacon.LocationY = Convert.ToDecimal(form.Get("txtCordinateY").ToString());
                    new CodeBeacon().insert(beacon);
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("Create");
            } 
            		
         }


}
}
