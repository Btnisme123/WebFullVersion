using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham_MVC.Areas.Admin.Models;
using  ShopMyPham_MVC.Framework;

namespace ShopMyPham_MVC.Areas.Admin.Controllers
{
    [Author]
    public class PermissionController : Controller
    {
        ShopBanHangDbContext db = new ShopBanHangDbContext();

        //
        // GET: /Admin/Permission/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PermissionGroup(int? groupId)
        {
            List<UserGroup> lstUserGroup = db.UserGroups.Where(x => x.ID != 1).OrderBy(x => x.ID).ToList();
            if (groupId == null)
            {
                groupId = lstUserGroup[0].ID;
            }
            string groupName = db.UserGroups.Find(groupId).Name;
            ViewBag.lstUserGroup = lstUserGroup;
            ViewBag.groupName = groupName;

            //Danh sách role (roleId) hiện tại của group => Chỉ lấy roleId
            List<string> lstRoleGroup = db.Group_Permission.Where(x => x.GroupId == groupId).Select(x => x.RoleId).ToList();

            List<PermissionModel> lstPermissionModel = new PermissionDao().GetListPermissionModel(lstRoleGroup);

            string currentRole = "";
            foreach (string roleId in lstRoleGroup)
            {
                currentRole += roleId + "*";
            }

            ViewBag.groupId = groupId;
            ViewBag.currentRole = currentRole;
            ViewBag.lstPermissionGroup = lstPermissionModel;
            return View();
        }

        [HttpPost]
        public ActionResult PermissionGroup(string lstRoleSelected, int groupId)
        {

            new GroupPermissionDao().UpdatePermissionGroup(lstRoleSelected, groupId);
            return RedirectToAction("PermissionGroup", new { groupId = groupId });
        }


        [HttpGet]
        public ActionResult PermissionUser(int? userId)
        {
            List<User> lstUser = db.Users.Where(x => x.ID != 1).OrderBy(x => x.ID).ToList();
            if (userId == null)
            {
                userId = lstUser[0].ID;
            }

            string userName = db.Users.Find(userId).Name;
            ViewBag.lstUser = lstUser;
            ViewBag.UserName = userName;

            //Danh sách role (roleId) hiện tại của user => Chỉ lấy roleId
            List<string> lstRoleUser = db.User_Permission.Where(x => x.UserID == userId).Select(x => x.RoleId).ToList();

            List<PermissionModel> lstPermissionModel = new PermissionDao().GetListPermissionModel(lstRoleUser);

            string currentRole = "";
            foreach (string roleId in lstRoleUser)
            {
                currentRole += roleId + "*";
            }

            ViewBag.userId = userId;
            ViewBag.currentRole = currentRole;
            ViewBag.lstPermissionGroup = lstPermissionModel;
            return View();
        }

        [HttpPost]
        public ActionResult PermissionUser(string lstRoleSelected, int userId)
        {

            new UserPermissionDao().UpdatePermissionUser(lstRoleSelected, userId);
            return RedirectToAction("PermissionUser", new { userId = userId });
        }
    }
}