using PagedList;
using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeOrderDetail
    {
        private ShopBanHangDbContext db= null;

        public CodeOrderDetail()
        {
            db = new ShopBanHangDbContext();
        }

        public IEnumerable<OrderDetailModel> ListPaging(long OrderID,int page,int pagesize)
        {
            var order = db.Orders.Find(OrderID);
            var model = from a in db.OrderDetails
                        join b in db.Orders on a.OrderID equals b.ID
                        join c in db.Products on a.ProductID equals c.ID
                        where a.OrderID == order.ID
                        select new OrderDetailModel()
                        {
                            ID = a.ID,
                            ProductName = c.Name,
                            Quantity = a.Quantity.Value,
                            Price = a.Price.Value,
                        };        
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}