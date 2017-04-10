using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham_MVC.Areas.Admin.Models;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeShop
    {
        private ShopBanHangDbContext db = null;

        public CodeShop()
        {
            db = new ShopBanHangDbContext();
        }

        public void Add(Shop shop)
        {
            db.Shops.Add(shop);
            db.SaveChanges();
        }
        public void Update(Shop Shop)
        {
            var result = db.Shops.Find(Shop.ID);
            result.ID = Shop.ID;
            result.UserID = Shop.UserID;
            result.Description = Shop.Description;
            result.Email = Shop.Email ;
            result.Logo = Shop.Logo;
            result.Address = Shop.Address;
            result.PhoneNumber = Shop.PhoneNumber;
            result.Name = Shop.Name;
            db.SaveChanges();

        }
        public void Delete(long Id)
        {        
                var shop = db.Shops.Find(Id);
                db.Shops.Remove(shop);
                db.SaveChanges(); 
        }

        public List<Shop> ListAll(ref int totalRecord, int pageIndex = 1, int pageSize = 12)
        {
            totalRecord = db.Shops.Where(x => x.ID > 0).Count();
            var model = db.Shops.Where(x => x.ID > 0).OrderByDescending(y => y.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }

        public IEnumerable<ShopModel> GetAllShop()
        {
            var model = from a in db.Shops

                        select new ShopModel()
                        {
                            ID = a.ID,                                   
                            Name = a.Name,  
                            Description = a.Description,
                            Address = a.Address,
                            UserID=a.UserID
                        };
            return model.OrderBy(x => x.ID).ToList();

        }

        public List<Shop> ListAll()
        {
            return db.Shops.Where(x => x.ID > 0).ToList();
        }

        public Shop ViewDetail(long Id)
        {
            return db.Shops.Find(Id);
        }
    }
}