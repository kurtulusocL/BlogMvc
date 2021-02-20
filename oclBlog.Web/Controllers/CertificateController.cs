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
    public class CertificateController : Controller
    {
        ApplicationDbContext _db;

        public CertificateController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/certificate/my-all-certificates")]
        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var certificate = _db.Certificates.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 21);
                return View(certificate);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var certificate = _db.Certificates.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(certificate);
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
        public ActionResult Create(Certificate model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/certificate/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Certificates.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var editCertificate = _db.Certificates.FirstOrDefault(i => i.Id == id);
                if (editCertificate != null)
                {
                    return View(editCertificate);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Certificate model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/certificate/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Certificates.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteCertificate = _db.Certificates.Find(id);
                if (deleteCertificate != null)
                {
                    _db.Certificates.Remove(deleteCertificate);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}