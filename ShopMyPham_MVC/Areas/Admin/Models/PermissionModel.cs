using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class PermissionModel
    {
        public string roleTable;
        public List<RoleModel> lstRoleModel;
        public int CheckAll;
    }
}