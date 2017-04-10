using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Reflection;
using ShopMyPham_MVC.Common;

namespace ShopMyPham_MVC.Areas.Admin.Models
{
    public class Author : ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session[CommonConstants.USER_SESSION]!=null)
            {
                var groupID = ((UserLogin)HttpContext.Current.Session[CommonConstants.USER_SESSION]).GroupID;
                if (groupID != "1")
                {
                    List<string> strRole = (List<string>)HttpContext.Current.Session["strRole"];
                    string role = "";
                    foreach (var item in strRole)
                    {
                        role += item;
                    }
                    string actionname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" +
                        filterContext.ActionDescriptor.ActionName;
                    if (!role.Contains(actionname))
                    {
                        filterContext.Result = new RedirectResult("~/Admin/Home/Warning");
                    }
                }
            }
            //
           else filterContext.Result = new RedirectResult("~/Admin/Login/Login");

        }

    }
}