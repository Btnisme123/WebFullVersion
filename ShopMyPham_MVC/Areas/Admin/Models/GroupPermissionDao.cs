using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class GroupPermissionDao
    {
        ShopBanHangDbContext db = null;

        public GroupPermissionDao()
        {
            db = new ShopBanHangDbContext();
        }
        public void UpdatePermissionGroup(string lstRoleSelected, int groupId)
        {
            string[] arrRole = lstRoleSelected.Split('*');
            IEnumerable<Group_Permission> lstGroupPermission = db.Group_Permission.Where(x => x.GroupId == groupId);
            db.Group_Permission.RemoveRange(lstGroupPermission);
            db.SaveChanges();
            for (int i = 0; i < arrRole.Length; i++)
            {
                if (arrRole[i].Length > 0)
                {
                    Group_Permission gp = new Group_Permission();
                    gp.GroupId = groupId;
                    gp.RoleId = arrRole[i];
                    db.Group_Permission.Add(gp);
                }
            }
            db.SaveChanges();
        }
    }
}
