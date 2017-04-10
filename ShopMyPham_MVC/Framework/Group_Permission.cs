namespace ShopMyPham_MVC.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group_Permission
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }

        [StringLength(50)]
        public string RoleId { get; set; }
    }
}
