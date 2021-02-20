using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ocLBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SocialMediaController : Controller
    {
        ApplicationDbContext _db;

        public SocialMediaController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas()
        {
            using (_db = new ApplicationDbContext())
            {
                var social = _db.SocialMedias.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
                return View(social);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SocialMedia model)
        {
            using (_db = new ApplicationDbContext())
            {
                _db.SocialMedias.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();

                return RedirectToAction("yonas");
            }
        }

        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var social = _db.SocialMedias.FirstOrDefault(i => i.Id == id);
                if (social != null)
                {
                    return View(social);
                }
                return RedirectToAction("yonas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SocialMedia model)
        {
            using (_db = new ApplicationDbContext())
            {
                _db.SocialMedias.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("yonas");
            }
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var socialDelete = _db.SocialMedias.Find(id);
                if (socialDelete != null)
                {
                    _db.SocialMedias.Remove(socialDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}