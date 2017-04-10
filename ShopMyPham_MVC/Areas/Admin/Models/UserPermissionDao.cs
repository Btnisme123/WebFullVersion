using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class UserPermissionDao
    {
        ShopBanHangDbContext db = null;

        public UserPermissionDao()
        {
            db = new ShopBanHangDbContext();
        }
        public void UpdatePermissionUser(string lstRoleSelected, int userId)
        {
            string[] arrRole = lstRoleSelected.Split('*');
            IEnumerable<User_Permission> lstUserPermission = db.User_Permission.Where(x => x.UserID == userId);
            db.User_Permission.RemoveRange(lstUserPermission);
            db.SaveChanges();
            for (int i = 0; i < arrRole.Length; i++)
            {
                if (arrRole[i].Length > 0)
                {
                    User_Permission up = new User_Permission();
                    up.UserID = userId;
                    up.RoleId = arrRole[i];
                    db.User_Permission.Add(up);
                }
            }
            db.SaveChanges();
        }
    }
}
