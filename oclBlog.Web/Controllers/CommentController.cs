using ocLBlog.Data.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ocLBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        ApplicationDbContext _db;

        public CommentController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult ArticleComment(int? id)
        {
            using (_db=new ApplicationDbContext())
            {
                var comment = _db.Comments.Include("Article").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ArticleId == id).OrderByDescending(i => i.CreatedDate).Take(50).ToList();

                return PartialView("_ArticleComment", comment);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db=new ApplicationDbContext())
            {
                var comment = _db.Comments.Include("Article").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(comment);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CommentDetail(int id)
        {
            return View(_db.Comments.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var commentList = _db.Comments.Include("Article").Where(i => i.IsDeleted == false && i.IsConfirm == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(commentList);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetConfirm(int id)
        {
            var confirm = _db.Comments.FirstOrDefault(i => i.Id == id);
            confirm.IsConfirm = true;
            _db.SaveChanges();

            return RedirectToAction("ConfirmList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deleteComment = _db.Comments.Find(id);
                if (deleteComment!=null)
                {
                    _db.Comments.Remove(deleteComment);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}