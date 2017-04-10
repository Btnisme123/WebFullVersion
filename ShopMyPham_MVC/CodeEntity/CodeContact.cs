using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeContact
    {
        private ShopBanHangDbContext context = null;

        public CodeContact()
        {
            context = new ShopBanHangDbContext();
        }

        public int InsertFeedBack(Feedback fb)
        {
            context.Feedbacks.Add(fb);
            context.SaveChanges();
            return fb.ID;
        }

        public IEnumerable<FeedbackModel> GetAllFeedback()
        {
            var model = from a in context.Feedbacks

                        select new FeedbackModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Phone = a.Phone,
                            Email = a.Email,
                            Content = a.Content,
                            UserID = a.UserID,
                            CreatedDate = a.CreatedDate
                        };
            return model.ToList();

        }
    }
}