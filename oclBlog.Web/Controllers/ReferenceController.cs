using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ocLBlog.Web.Controllers
{
    public class ReferenceController : Controller
    {
        ApplicationDbContext _db;

        public ReferenceController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/my-references/all-my-references")]
        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var refere = _db.References.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 21);
                return View(refere);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var refere = _db.References.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(refere);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reference model)
        {
            if (ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.References.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var editRefere = _db.References.FirstOrDefault(i => i.Id == id);
                if (editRefere != null)
                {
                    return View(editRefere);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reference model)
        {
            if (ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.References.Attach(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteRefere = _db.References.Find(id);
                if (deleteRefere != null)
                {
                    _db.References.Remove(deleteRefere);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}