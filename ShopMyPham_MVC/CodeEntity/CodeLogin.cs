using ShopMyPham_MVC.Common;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeLogin
    {
        private ShopBanHangDbContext db = null;


        public CodeLogin()
        {
            db = new ShopBanHangDbContext();
        }

        public int Login(string Username, string Password, bool isAdminLogin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == Username);
            if (result == null)
            {
                return 0;
            }
            else if (result.Password == Password)
                return 1;
            else return 0;
        }

        public int LoginAccount(string Username, string Password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == Username);
            if (result == null)
            {
                return 0;
            }

            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == Password)
                        return 1;
                    else
                        return -2;
                }
            }

        }

    }
}