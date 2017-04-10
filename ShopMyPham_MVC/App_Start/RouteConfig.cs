using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopMyPham_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "Home",
            url: "trang-chu",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );
            routes.MapRoute(
            name: "Product Detail",
            url: "san-pham/{ShortName}-{id}",
            defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
             namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );
            routes.MapRoute(
            name: "Product All",
            url: "san-pham",
            defaults: new { controller = "Product", action = "PostAll", id = UrlParameter.Optional },
            namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );
            routes.MapRoute(
            name: "Liên hệ",
            url: "lien-he",
            defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );
            routes.MapRoute(
            name: "Tìm kiếm",
            url: "tim-kiem",
            defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
            namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );
            routes.MapRoute(
            name: "Product Category",
            url: "{ShortName}-{id}",
            defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
            namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopMyPham_MVC.Controllers" }
            );
        }
    }
}
