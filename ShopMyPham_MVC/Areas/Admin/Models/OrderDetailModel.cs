using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class OrderDetailModel
    {
        public long ID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}