using PagedList;
using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeProduct
    {
        private ShopBanHangDbContext db = null;

        public CodeProduct()
        {
            db = new ShopBanHangDbContext();
        }

        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
        public void Update(Product product)
        {
            var result = db.Products.Find(product.ID);
            result.Name = product.Name;
            result.ShortName = product.ShortName;
            result.Description = product.Description;
            result.Image = product.Image;
            result.Price = product.Price;
            result.PromotionPrice = product.PromotionPrice;
            result.IncludedVAT = product.IncludedVAT;
            result.Quantity = product.Quantity;
            result.CategoryID = product.CategoryID;
            result.Detail = product.Detail;
            result.TopHot = product.TopHot;
            result.View = product.View;
            db.SaveChanges();

        }
        public void Delete(long Id)
        {
            if (DeleteOderDetail(Id))
            {
                var product = db.Products.Find(Id);
                db.Products.Remove(product);
                db.SaveChanges();
            }

        }
        public bool DeleteOderDetail(long id)
        {
            try
            {
                var product = db.OrderDetails.Where(x => x.ProductID == id).ToList();
                if (product.Count() > 0)
                {
                    db.OrderDetails.RemoveRange(product);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Product ViewDetail(long Id)
        {
            return db.Products.Find(Id);
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public IEnumerable<ProductModel> GetAllProduct()
        {
            var model = from a in db.Products
                        join b in db.Categories
                        on a.CategoryID equals b.ID
                        select new ProductModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Price = a.Price.Value.ToString(),
                            Quantity = a.Quantity.Value.ToString(),
                            Category = b.Name,
                            CreatedDate = a.CreatedDate.Value

                        };
            return model.OrderBy(x => x.ID).ToList();

        }

        public List<Product> GetFeatureProduct()
        {
            return db.Products.Where(x => x.TopHot != null).OrderBy(x => x.ID).Take(4).ToList();
        }
        public List<Product> GetNewProduct()
        {
            return db.Products.Where(x => x.ID > 0).OrderBy(x => x.ID).Take(4).ToList();
        }
        public List<Product> ListProductCategory(int ID, ref int totalRecord, int pageIndex = 1, int pageSize = 12)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == ID).Count();
            var model = db.Products.Where(x => x.CategoryID == ID).OrderByDescending(y => y.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
        public List<Product> ListSearchProduct(string name, ref int totalRecord, int pageIndex = 1, int pageSize = 12)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(name)).Count();
            var model = db.Products.Where(x => x.Name.Contains(name)).OrderByDescending(y => y.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
        public List<Product> ListAll(ref int totalRecord, int pageIndex = 1, int pageSize = 12)
        {
            totalRecord = db.Products.Where(x => x.ID > 0).Count();
            var model = db.Products.Where(x => x.ID > 0).OrderByDescending(y => y.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
    }
}