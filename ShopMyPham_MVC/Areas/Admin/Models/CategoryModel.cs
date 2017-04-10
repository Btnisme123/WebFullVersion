using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    [Serializable]
    public class CategoryModel
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}