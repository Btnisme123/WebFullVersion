using PagedList;
using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeCategory
    {
        private ShopBanHangDbContext db = null;

        public CodeCategory()
        {
            db = new ShopBanHangDbContext();
        }

        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }
        public void Insert(Category category)
        {
            category.CreatedDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();

        }
        public void Update(Category entity)
        {

            var category = db.Categories.Find(entity.ID);
            category.Name = entity.Name;
            category.ModifiedDate = DateTime.Now;
            db.SaveChanges();

        }
        //public IEnumerable<Category> ListPaging(string searchString, int page, int pagesize)
        //{
        //    IQueryable<Category> model = db.Categories;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString) && x.Status == true);
        //    }
        //    return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pagesize);
        //}

        public Category ViewDetail(long ID)
        {
            return db.Categories.Find(ID);
        }

        public void Delete(long ID)
        {

            var category = db.Categories.Find(ID);
            db.Categories.Remove(category);
            db.SaveChanges();


        }
        public IEnumerable<CategoryModel> ListAllCategory(int page)
        {
            var model = from a in db.Categories
                        join b in db.Users
                        on a.CreatedBy equals b.ID
                        select new CategoryModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            CreatedDate = a.CreatedDate.Value,
                            UserName = b.UserName
                        };
            model.OrderBy(x => x.ID).Skip(page * 10).Take(10);
            return model.ToList();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.ID > 0).ToList();
        }
    }
}