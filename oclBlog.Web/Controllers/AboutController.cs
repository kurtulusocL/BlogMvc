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
    public class AboutController : Controller
    {
        ApplicationDbContext _db;

        public AboutController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/about/kurtulus-ocal")]
        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
                return View(about);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas()
        {
            using (_db = new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
                return View(about);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AboutDetail(int id)
        {
            return View(_db.Abouts.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(About model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/about/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Abouts.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var editAbout = _db.Abouts.FirstOrDefault(i => i.Id == id);
                if (editAbout!=null)
                {
                    return View(editAbout);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(About model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/about/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Abouts.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deleteAbout = _db.Abouts.Find(id);
                if (deleteAbout!=null)
                {
                    _db.Abouts.Remove(deleteAbout);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}