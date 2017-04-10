using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    [Serializable]
    public class ProductModel
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public string Price { get; set; }

        public string Quantity { get; set; }

        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}