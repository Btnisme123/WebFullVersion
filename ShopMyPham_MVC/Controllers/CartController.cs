
using ShopMyPham_MVC.CodeEntity;
using ShopMyPham_MVC.Common;
using ShopMyPham_MVC.Framework;
using ShopMyPham_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopMyPham_MVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult AddItem(long productId, int quantity)
        {
            var product = new CodeProduct().ViewDetail(productId);
            var cart = Session[Common.CommonConstants.CartSession];
            if(cart!=null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x=>x.Product.ID==productId))
                {
                    foreach(var item in list)
                    {
                        if(item.Product.ID==productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CommonConstants.CartSession] = list;
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpGet]
        public ActionResult Payments()
        {
            UserLogin session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session==null)
            {
               return RedirectToAction("Login", "Home");
            }
            else
            {
                var order = new Order();
                order.CustomerID = session.UserID;
                order.CreatedDate = DateTime.Now;
                order.Status = false;
                try
                {
                    long id = new CodeOrder().Insert(order);
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    var detailDao = new CodeOrderDetail();
                    decimal total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = id;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Quantity = item.Quantity;
                        detailDao.Insert(orderDetail);

                        total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    }
                    //var model = new CodeUser().ViewDetailID(session.UserID);

                    //string content = System.IO.File.ReadAllText(Server.MapPath("/Accset/Client/template/neworder.html"));

                    //content = content.Replace("{{CustomerName}}", model.Name);
                    //content = content.Replace("{{Phone}}", model.Phone);
                    //content = content.Replace("{{Email}}", model.Email);
                    //content = content.Replace("{{Address}}", model.Address);
                    //content = content.Replace("{{Total}}", total.ToString("N0"));
                    //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    //new MailHelper().SendMail(model.Email, "Đơn hàng mới từ ShopElectronic", content);
                    //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ ShopElectronic", content);
       
                }
                catch (Exception ex)
                {
                    //ghi log
                    return Redirect("/loi-thanh-toan");
                }
                return RedirectToAction("Success", "Cart");
            }

           
        }

        public ActionResult Success()
        {
            return View();
        }

    }
}