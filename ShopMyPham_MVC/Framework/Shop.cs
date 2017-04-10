using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Framework
{

    [Table("Shop")]
    public class Shop
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public String Logo { get; set; }
        
        public String Name { get; set; }
        public String Address { get; set; }

        public String PhoneNumber { get; set; }
        public String Description { get; set; }

        public String Email { get; set; }

        public int UserID { get; set; }
    }
}