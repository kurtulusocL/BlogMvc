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
    public class CategoryController : Controller
    {
        ApplicationDbContext _db;

        public CategoryController()
        {
            _db = new ApplicationDbContext();
        }       
       
        public ActionResult CategoryArticle(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var articleCate = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.CategoryId == id && i.IsDeleted == false).OrderByDescending(i => i.Hit.ToString()).ToPagedList(page, 26);
                return View(articleCate);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var cate = _db.Categories.Include("Articles").Where(i => i.IsDeleted == false).OrderBy(i => i.Articles.Count()).ToPagedList(page, 20);
                return View(cate);
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
        public ActionResult Create(Category model)
        {
            using (_db = new ApplicationDbContext())
            {
                _db.Categories.Add(model);
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
                var cateEdit = _db.Categories.FirstOrDefault(i => i.Id == id);
                if (cateEdit != null)
                {
                    return View(cateEdit);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            using (_db = new ApplicationDbContext())
            {
                _db.Categories.Attach(model);
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
                var cateDelete = _db.Categories.Find(id);
                if (cateDelete != null)
                {
                    _db.Categories.Remove(cateDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}