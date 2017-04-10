using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeType
    {
        private ShopBanHangDbContext db = null;

        public CodeType()
        {
            db = new ShopBanHangDbContext();
        }


    }
}