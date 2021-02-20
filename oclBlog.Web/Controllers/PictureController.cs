using ocLBlog.Data.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ocLBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PictureController : Controller
    {
        ApplicationDbContext _db;

        public PictureController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var photo = _db.Pictures.Include("Article").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40);
                return View(photo);
            }
        }

        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deletePhoto = _db.Pictures.Find(id);
                if (deletePhoto!=null)
                {
                    _db.Pictures.Remove(deletePhoto);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}