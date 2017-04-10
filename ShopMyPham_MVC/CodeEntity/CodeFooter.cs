using PagedList;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeFooter
    {
        private ShopBanHangDbContext context = null;

        public CodeFooter()
        {
            context = new ShopBanHangDbContext();
        }
        public Footer GetById(string id)
        {
            return context.Footers.Find(id);
        }
        public string Insert(Footer entity)
        {
            context.Footers.Add(entity);
            context.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<Footer> ListPaging(string searchString, int page, int pagesize)
        {
            IQueryable<Footer> model = context.Footers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Content.Contains(searchString) && (x.Status == true));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
        public bool Update(Footer entity)
        {
            try
            {
                var footer = context.Footers.Find(entity.ID);
                footer.Content = entity.Content;
                footer.Status = entity.Status;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Footer ViewDetail(string ID)
        {
            return context.Footers.Find(ID);
        }
        public bool Delete(string id)
        {
            try
            {
                var footer = context.Footers.Find(id);
                context.Footers.Remove(footer);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Footer GetFooter()
        {
            return context.Footers.SingleOrDefault(x => x.Status == true);
        }
    }
}