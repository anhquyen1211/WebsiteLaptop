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
    public class GenreController : Controller
    {
        private LQShopDb db = new LQShopDb();
        // GET: Admin/Genre
        public ActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.search = search;
            var list = db.Genres.OrderBy(a => a.Create_at);
            if (!string.IsNullOrEmpty(search))
            {
                list = db.Genres.Where(a => a.Genre_name.Contains(search)).OrderBy(a => a.Create_at);
            }
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Create(string genreName, Genre genre)
        {
            string result = "false";
            try
            {
                Genre checkExist = db.Genres.SingleOrDefault(m => m.Genre_name == genreName);
                if (checkExist != null)
                {
                    result = "exist";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                genre.Genre_name = genreName;
                genre.Create_by = User.Identity.GetEmail();
                genre.Update_by = User.Identity.GetEmail();
                genre.Create_at = DateTime.Now;
                genre.Update_at = DateTime.Now;
                db.Genres.Add(genre);
                db.SaveChanges();
                result = "success";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id, string genreName)
        {
            string result = "error";
            Genre genre = db.Genres.FirstOrDefault(m => m.Genre_id == id);
            var checkExist = db.Genres.SingleOrDefault(m => m.Genre_name == genreName);
            try
            {
                if (checkExist != null)
                {
                    result = "exist";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                result = "success";
                genre.Genre_name = genreName;
                genre.Update_at = DateTime.Now;
                genre.Update_by = User.Identity.GetEmail();
                UpdateModel(genre);
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            string result = "error";
            Genre genre = db.Genres.FirstOrDefault(m => m.Genre_id == id);
            try
            {
                result = "delete";
                db.Genres.Remove(genre);
                db.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}