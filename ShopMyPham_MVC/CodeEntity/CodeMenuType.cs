using PagedList;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeMenuType
    {
        private ShopBanHangDbContext context = null;

        public CodeMenuType()
        {
            context = new ShopBanHangDbContext();
        }
        public MenuType GetById(int id)
        {
            return context.MenuTypes.Find(id);
        }
        public int Insert(MenuType entity)
        {
            context.MenuTypes.Add(entity);
            context.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<MenuType> ListPaging(string searchString, int page, int pagesize)
        {
            IQueryable<MenuType> model = context.MenuTypes;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
        public bool Update(MenuType entity)
        {
            try
            {
                var menutype = context.MenuTypes.Find(entity.ID);
                menutype.Name = entity.Name;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public MenuType ViewDetail(int ID)
        {
            return context.MenuTypes.Find(ID);
        }
        public bool Delete(int id)
        {
            try
            {
                var menutype = context.MenuTypes.Find(id);
                context.MenuTypes.Remove(menutype);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public List<MenuType> ListAll()
        {
            return context.MenuTypes.Where(x => x.Name != null).ToList();
        }
    }
}