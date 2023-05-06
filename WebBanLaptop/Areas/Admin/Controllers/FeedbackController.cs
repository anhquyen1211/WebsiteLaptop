using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Common.Helpers;
using WebBanLaptop.Models;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class FeedbackController : Controller
    {
        LQShopDb db = new LQShopDb();
        // GET: Admin/Feedback
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = db.Feedbacks.OrderByDescending(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Feedbacks.Where(a => a.Account_id.ToString().Contains(search)).OrderByDescending(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReplyComment(int id, string reply_content, ReplyFeedback reply)
        {
            bool result = false;
            if (User.Identity.IsAuthenticated)
            {
                reply.Feedback_id = id;
                reply.Account_id = User.Identity.GetUserID();
                reply.Content = reply_content;
                reply.Status = "2";
                reply.Create_at = DateTime.Now;
                db.ReplyFeedbacks.Add(reply);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}