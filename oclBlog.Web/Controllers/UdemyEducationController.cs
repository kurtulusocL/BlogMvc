using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace oclBlog.Web.Controllers
{
    public class UdemyEducationController : Controller
    {
        ApplicationDbContext _db;

        public UdemyEducationController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/ocl-udemy/udemy-online-lesson")]
        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var udemy = _db.Udemies.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 15);
                return View(udemy);
            }
        }

        [Route("~/lessondetail/{newsTitle}-{id:int}")]
        public ActionResult Detail(int id, string seo_text)
        {
            Udemy udemyEducation = new Udemy();
            var eduDetail = _db.Udemies.Where(i => i.Id == id).FirstOrDefault();
            udemyEducation = eduDetail;
            return View(udemyEducation);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var udemy = _db.Udemies.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 15);
                return View(udemy);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UdemyDetail(int id)
        {
            return View(_db.Udemies.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Udemy model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/udemy/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Udemies.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var udemyEdit = _db.Udemies.FirstOrDefault(i => i.Id == id);
                if (udemyEdit!=null)
                {
                    return View(udemyEdit);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Udemy model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/udemy/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Udemies.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var udemyDelete = _db.Udemies.Find(id);
                if (udemyDelete!=null)
                {
                    _db.Udemies.Remove(udemyDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}