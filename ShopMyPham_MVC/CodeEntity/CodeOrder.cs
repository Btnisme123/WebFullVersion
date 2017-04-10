using PagedList;
using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeOrder
    {
        private ShopBanHangDbContext db = null;
        public CodeOrder()
        {
            db = new ShopBanHangDbContext();
        }

        public IEnumerable<OrderModel> ListPaging(string searchString, int page, int pagesize)
        {
            var model = from a in db.Users
                        join b in db.Orders
                        on a.ID equals b.CustomerID
                        select new OrderModel()
                        {
                            ID = b.ID,
                            NameCustomer = a.Name,
                            Mobile = a.Phone,
                            Address = a.Address,
                            Email = a.Email,
                            CreatedDate=b.CreatedDate.Value,
                            Status = b.Status
                        };

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NameCustomer.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
        //else if(checkOrder==true)
        //{
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.NameCustomer.Contains(searchString) && x.Status==true);
        //    }
        //    return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        //}
        //else
        //{
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.NameCustomer.Contains(searchString) && x.Status == false);
        //    }
        //    return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        //}
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public bool ChangeStatus(long id)
        {
            var Order = db.Orders.Find(id);
            Order.Status = !Order.Status;
            db.SaveChanges();
            return Order.Status;
        }
    }
 

}
