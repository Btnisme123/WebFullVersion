using PagedList;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeBanner
    {
        private ShopBanHangDbContext db = null;
        public CodeBanner()
        {
            db = new ShopBanHangDbContext();
        }
        public Banner GetById(int id)
        {
            return db.Banners.Find(id);
        }
        public int Insert(Banner banner)
        {
            db.Banners.Add(banner);
            db.SaveChanges();
            return banner.ID;
        }

        public bool Update(Banner banner)
        {
            try
            {
                var editBanner = db.Banners.Find(banner.ID);
                editBanner.Image = banner.Image;
                editBanner.Description = banner.Description;
                editBanner.TypeID = banner.TypeID;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Banner> ListPaging(string searchString, int page, int pagesize)
        {
            IQueryable<Banner> model = db.Banners;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Description.Contains(searchString));
            }
            return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pagesize);
        }
        public bool Delete(int id)
        {
            try
            {
                var banner = db.Banners.Find(id);
                db.Banners.Remove(banner);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Banner ViewDetail(int ID)
        {
            return db.Banners.Find(ID);
        }
        public List<TypeBanner> ListAll()
        {
            return db.TypeBanners.Where(x => x.ID > 0).ToList();
        }
        public List<Banner> GetBannerHome(int id)
        {
            return db.Banners.Where(x => x.TypeID == id).Take(4).ToList();
        }
    }
}