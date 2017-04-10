using PagedList;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeUser
    {
        private ShopBanHangDbContext db = null;

        public CodeUser()
        {
            db = new ShopBanHangDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<User> ListPaging(string searchString, int page, int pagesize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public User ViewDetail(int ID)
        {
            return db.Users.Find(ID);
        }
        public User ViewDetailID(long ID)
        {
            return db.Users.Find(ID);
        }
        public User GetByID(string Username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == Username);
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                if(user.GroupID!="ADMIN")
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}