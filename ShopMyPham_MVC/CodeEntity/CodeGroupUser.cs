using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeGroupUser
    {

        private ShopBanHangDbContext db = null;

        public CodeGroupUser()
        {
            db = new ShopBanHangDbContext();

        }

        public List<UserGroup> ListAllGroup()
        {
            return db.UserGroups.ToList();

        }
    }
}