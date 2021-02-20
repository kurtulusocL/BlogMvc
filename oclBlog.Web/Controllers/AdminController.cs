using ocLBlog.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ocLBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminList()
        {
            using (_db = new ApplicationDbContext())
            {
                var admin = _db.Users.OrderByDescending(i => i.Id).ToList();
                return View(admin);
            }
        }

        public ActionResult Delete(string id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteAdmin = _db.Users.Find(id);
                if (deleteAdmin != null)
                {
                    _db.Users.Remove(deleteAdmin);
                    _db.SaveChanges();
                }
                return RedirectToAction("AdminList");
            }
        }

        public ActionResult AdminInfo(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                var adminInform = _db.Users.Where(i => i.Id == userID).OrderBy(i => i.NameLastname).ToList();
                return PartialView("_AdminInfo", adminInform);
            }
        }

        public ActionResult AdminInformation(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                var adminInfo = _db.Users.Where(i => i.Id == userID).OrderBy(i => i.NameLastname).ToList();
                return PartialView("_AdminInformation", adminInfo);
            }
        }

        public ActionResult _AdminIstatistic()
        {
            return View();
        }

        public ActionResult TotalArticle(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                var article = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false && i.AdminId == userID).OrderByDescending(i => i.CreatedDate).ToList();

                return PartialView("_TotalArticle", article);
            }
        }

        public ActionResult TotalPicture(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                var articlePhoto = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false && i.AdminId == userID).OrderByDescending(i => i.CreatedDate).ToList();
                return PartialView("_TotalPicture", articlePhoto);
            }
        }

        public ActionResult _TotalSkill(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);           
            var skills = _db.Skills.Where(i => i.IsDeleted == false && i.AdminId == userID).SingleOrDefault();
            return View(skills);
        }

        public ActionResult TotalCategory(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);

            using (_db = new ApplicationDbContext())
            {
                var category = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false && i.AdminId == userID && i.Category.Articles.Count() > 0).OrderByDescending(i => i.Category.Articles.Count()).ToList();
                return PartialView("_TotalCategory", category);
            }

        }

        public ActionResult TotalCategoryArticle(string userID)
        {
            userID = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                using (_db = new ApplicationDbContext())
                {
                    var article = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false && i.AdminId == userID).OrderByDescending(i => i.CreatedDate).ToList();

                    return PartialView("_TotalCategoryArticle", article);
                }
            }
        }

        public ActionResult ContactAdmin()
        {
            using (_db=new ApplicationDbContext())
            {
                var contact = _db.Contacts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(10).ToList();
                return PartialView("_ContactAdmin", contact);
            }
        }

        public ActionResult LearnPriceAdmin()
        {
            using (_db=new ApplicationDbContext())
            {
                var learnPrice = _db.LearnPrices.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(10).ToList();
                return PartialView("_LearnPriceAdmin", learnPrice);
            }
        }

        public ActionResult CommentAdmin()
        {
            using (_db=new ApplicationDbContext())
            {
                var comment = _db.Comments.Include("Article").Where(i => i.IsDeleted == false && i.IsConfirm == false).OrderByDescending(i => i.CreatedDate).Take(10).ToList();
                return PartialView("_CommentAdmin", comment);
            }
        }
    }
}