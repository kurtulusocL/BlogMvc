using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ocLBlog.Web.Controllers
{
    public class PortfolioController : Controller
    {
        ApplicationDbContext _db;

        public PortfolioController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/portfolio/all-my-portfolios")]
        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var port = _db.Portfolios.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 21);
                return View(port);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var port = _db.Portfolios.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(port);
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
        public ActionResult Create(Portfolio model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/port/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Portfolios.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var editPort = _db.Portfolios.FirstOrDefault(i => i.Id == id);
                if (editPort != null)
                {
                    return View(editPort);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Portfolio model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/port/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Portfolios.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deletePortfolio = _db.Portfolios.Find(id);
                if (deletePortfolio!=null)
                {
                    _db.Portfolios.Remove(deletePortfolio);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}