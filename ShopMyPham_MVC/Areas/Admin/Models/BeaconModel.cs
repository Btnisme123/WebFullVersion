using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class BeaconModel
    {
        public String MACID { get; set; }
        public decimal LocationX { get; set; }
        public decimal LocationY { get; set; }
        public Int64 ShopID { get; set; }
        public String Information { get; set; }
        public String Name { get; set; }
    }
}