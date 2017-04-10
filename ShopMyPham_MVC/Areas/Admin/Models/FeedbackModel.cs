using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class FeedbackModel
    {
        public int ID { get; set; }

      
        public string Name { get; set; }

      
        public string Phone { get; set; }

       
        public string Email { get; set; }

      
        public string Address { get; set; }

     
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }

        public int UserID { get; set; }
    }
}