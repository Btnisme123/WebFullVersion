namespace ShopMyPham_MVC.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeBanner")]
    public partial class TypeBanner
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}
