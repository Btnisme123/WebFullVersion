using PagedList;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeMenu
    {
        private ShopBanHangDbContext db = null;
        public CodeMenu()
        {
            db = new ShopBanHangDbContext();
        }
        public Menu GetById(int id)
        {
            return db.Menus.Find(id);
        }
        public long Insert(Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
            return menu.ID;
        }

        public bool Update(Menu menu)
        {
            try
            {
                var editmenu = db.Menus.Find(menu.ID);
                editmenu.Text = menu.Text;
                editmenu.Link = menu.Link;
                editmenu.DisplayOrder = menu.DisplayOrder;
                editmenu.Target = menu.Target;
                editmenu.Status = menu.Status;
                editmenu.TypeID = menu.TypeID;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Menu> ListPaging(string searchString, int page, int pagesize)
        {
            IQueryable<Menu> model = db.Menus;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString));
            }
            return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pagesize);
        }
        public bool Delete(int id)
        {
            try
            {
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Menu ViewDetail(int ID)
        {
            return db.Menus.Find(ID);
        }
        public List<Menu> ListgetById(int Id)
        {
            return db.Menus.Where(x => x.TypeID == Id).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}