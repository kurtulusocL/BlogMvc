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
    public class SkillController : Controller
    {
        ApplicationDbContext _db;

        public SkillController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/skills/all-my-skill")]
        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var skills = _db.Skills.Where(i => i.IsDeleted == false).OrderByDescending(i => i.Degree).ToList();
                return View(skills);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var skills = _db.Skills.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20);
                return View(skills);
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
        public ActionResult Create(Skill model)
        {
            using (_db = new ApplicationDbContext())
            {
                _db.Skills.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();

                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var skillEdit = _db.Skills.FirstOrDefault(i => i.Id == id);
                if (skillEdit != null)
                {
                    return View(skillEdit);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Skill model)
        {
            using (_db = new ApplicationDbContext())
            {
                _db.Skills.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var skillDelete = _db.Skills.Find(id);
                if (skillDelete != null)
                {
                    _db.Skills.Remove(skillDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}