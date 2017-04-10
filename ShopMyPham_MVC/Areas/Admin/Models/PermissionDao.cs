using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham_MVC.Framework;
namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class PermissionDao
    {
        ShopBanHangDbContext db = new ShopBanHangDbContext();

        public List<PermissionModel> GetListPermissionModel(List<string> lstRoleCurent)
        {
            //Danh sách RoleTable
            List<string> lstRoleTable = db.Roles.Select(r => r.RoleTable).Distinct().ToList();
            //Danh sách Role
            List<Role> lstRole = db.Roles.ToList();

            List<PermissionModel> lstPermissionModel = new List<PermissionModel>();

            //Add giá trị cho lstPermissionModel
            foreach (string roleTable in lstRoleTable)
            {
                PermissionModel prermissionModel = new PermissionModel();
                prermissionModel.roleTable = roleTable;
                prermissionModel.lstRoleModel = new List<RoleModel>();
                //Add giá trị cho prermissionModel.lstRoleModel
                List<Role> lstRoleOfTableRole = lstRole.Where(x => x.RoleTable == roleTable).ToList();
                int countLstRoleOfTableRole = lstRoleOfTableRole.Count;
                int temp = 0;
                foreach (Role role in lstRoleOfTableRole)
                {
                    RoleModel roleModel = new RoleModel();
                    roleModel.roleId = role.ID;
                    roleModel.name = role.Name;
                    if (lstRoleCurent.Contains(role.ID))
                    {
                        temp++;
                        roleModel.isChecked = true;
                    }
                    else
                    {
                        roleModel.isChecked = false;

                    }

                    prermissionModel.lstRoleModel.Add(roleModel);
                }
                //0=>chưa có role con nào được chọn ; 2=> tất cả được chọn, 1=> có item được chọn nhưng không phải tất cả
                if (temp == 0) prermissionModel.CheckAll = 0;
                else if (temp > 0 && temp < countLstRoleOfTableRole) prermissionModel.CheckAll = 1;
                else prermissionModel.CheckAll = 2;
                lstPermissionModel.Add(prermissionModel);
            }
            return lstPermissionModel;
        }
    }
}