using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class OrderModel
    {
        public long ID { get; set; }
        public string NameCustomer { get; set; }

        public string Mobile { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}