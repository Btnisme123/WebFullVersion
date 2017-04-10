using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeAbout
    {
        private ShopBanHangDbContext db = null;

        public CodeAbout()
        {
            db = new ShopBanHangDbContext();
        }
        public About GetById(int id)
        {
            return db.Abouts.Find(id);
        }
        public long Insert(About about)
        {
            about.CreatedDate = DateTime.Now;
            db.Abouts.Add(about);
            db.SaveChanges();
            return about.ID;
        }

        public bool Update(About about)
        {
            try
            {
                var editAbout = db.Abouts.Find(about.ID);
                editAbout.Name = about.Name;
                editAbout.MetaTitle = about.MetaTitle;
                editAbout.Description = about.Description;
                editAbout.Image = about.Image;
                editAbout.Detail = about.Detail;
                editAbout.Status = about.Status;
                editAbout.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<About> ListPaging(string searchString,int page,int pagesize)
        {
            IQueryable<About> model = db.Abouts;
            if(!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Detail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public bool Delete(int id)
        {
            try
            {
                var about = db.Abouts.Find(id);
                db.Abouts.Remove(about);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public About ViewDetail(int ID)
        {
            return db.Abouts.Find(ID);
        }
    }
}