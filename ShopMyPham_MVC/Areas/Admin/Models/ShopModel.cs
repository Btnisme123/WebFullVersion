using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    [Serializable]
    public class ShopModel
    {
        public Int64 ID { get; set; }
        public String Logo { get; set; }

        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Description { get; set; }

        public String Email { get; set; }

        public int UserID { get; set; }
    }
}