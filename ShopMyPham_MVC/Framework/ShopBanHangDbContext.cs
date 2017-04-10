namespace ShopMyPham_MVC.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopBanHangDbContext : DbContext
    {
        public ShopBanHangDbContext()
            : base("name=ShopBanHangDbContext2")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Group_Permission> Group_Permission { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Permission> User_Permission { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<TypeBanner> TypeBanners { get; set; }

        public virtual DbSet<Beacon> Beacons { get; set; }

        public virtual DbSet<Shop> Shops { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Permission>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User_Permission>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Beacon>()
              .Property(e => e.MACID).IsUnicode(false);
            
            modelBuilder.Entity<Beacon>()
                      .Property(e => e.LocationX)
                     .HasPrecision(18, 15);
            
            modelBuilder.Entity<Beacon>()
                         .Property(e => e.LocationY)
                      .HasPrecision(18, 15);
            
            modelBuilder.Entity<Beacon>()
                         .Property(e => e.ShopID);
            
            modelBuilder.Entity<Beacon>()
                         .Property(e => e.Name)
                         .IsUnicode(false);
            
            modelBuilder.Entity<Beacon>()
                         .Property(e => e.Information)
                         .IsUnicode(false);

            modelBuilder.Entity<Shop>()
                .Property(e => e.ID);

            modelBuilder.Entity<Shop>()
              .Property(e => e.UserID);

            modelBuilder.Entity<Shop>()
              .Property(e => e.Email)
              .IsUnicode(false); ;

            modelBuilder.Entity<Shop>()
              .Property(e => e.Description)
              .IsUnicode(false);     

            modelBuilder.Entity<Shop>()
              .Property(e => e.Logo);

            modelBuilder.Entity<Shop>()
             .Property(e => e.Name);

            modelBuilder.Entity<Shop>()
            .Property(e => e.PhoneNumber);

            modelBuilder.Entity<Shop>()
            .Property(e => e.Address);

            modelBuilder.Entity<Feedback>()
           .Property(e => e.ID);

             modelBuilder.Entity<Feedback>()
           .Property(e => e.Name);


            modelBuilder.Entity<Feedback>()
          .Property(e => e.Phone);


            modelBuilder.Entity<Feedback>()
          .Property(e => e.CreatedDate);

         
            modelBuilder.Entity<Feedback>()
          .Property(e => e.Status);


            modelBuilder.Entity<Feedback>()
          .Property(e => e.UserID);
        }
    }
}
