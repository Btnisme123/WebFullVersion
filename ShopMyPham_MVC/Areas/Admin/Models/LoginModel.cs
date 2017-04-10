using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nhập Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nhập Password")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}