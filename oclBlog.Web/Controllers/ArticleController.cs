using Newtonsoft.Json;
using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static ocLBlog.Web.Controllers.CaptchaResult;

namespace ocLBlog.Web.Controllers
{
    public class ArticleController : Controller
    {
        public class CaptchaResult
        {
            public class CaptchaResponse
            {
                [JsonProperty("success")]
                public bool Success { get; set; }

                [JsonProperty("error-codes")]
                public List<string> ErrorCodes { get; set; }
            }
        }

        ApplicationDbContext _db;

        public ArticleController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/my-articles/all-of-articles")]
        public ActionResult Index(int page = 1)
        {
            using (_db=new ApplicationDbContext())
            {
                var article = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false).OrderByDescending(i => i.Hit.ToString()).ToPagedList(page, 26);

                return View(article);
            }
        }

        [Route("~/detail/{newsTitle}-{id:int}")]
        public ActionResult Detail(int id, string seo_text)
        {
            Article art = new Article();
            var artDetail = _db.Article.Where(i => i.Id == id).FirstOrDefault();
            art = artDetail;
            return View(art);
        }

        public ActionResult _CreateComment(int? id)
        {
            Session["articleId"] = id;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateComment(Comment model)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LcEEtkUAAAAAFbRjuk_4USKLu-7TmkEnVhavMci";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
                TempData["Message"] = "Lütfen güvenliği doğrulayınız.";
            else
                TempData["Message"] = "Güvenlik başarıyla doğrulanmıştır.";

            if (ModelState.IsValid)
            {
                model.ArticleId = Convert.ToInt32(Session["articleId"]);
                using (_db=new ApplicationDbContext())
                {
                    _db.Comments.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    var result = _db.SaveChanges();

                    if (result>0)
                    {
                        TempData["success"] = "Yorumunuz alınmıştır. Teşekkür ederim.";
                    }
                    else
                    {
                        TempData["error"] = "Lütfen ilgili alanları kontrol ederek tekrar deneyiniz";
                    }
                }
            }
            return RedirectToAction("all-of-articles", "my-articles");
        }

        public ActionResult _HitRead(int? id)
        {
            var read = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.Id == id).SingleOrDefault();
            read.Hit++;
            _db.SaveChanges();

            return View(read);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var article = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);

                return View(article);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ArticleDetail(int id)
        {
            return View(_db.Article.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article model)
        {
            if (ModelState.IsValid)
            {
                using (_db=new ApplicationDbContext())
                {
                    _db.Article.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            using (_db=new ApplicationDbContext())
            {
                var editArticle = _db.Article.FirstOrDefault(i => i.Id == id);
                if (editArticle!=null)
                {
                    return View(editArticle);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article model)
        {
            if (ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.Article.Attach(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreatePhoto(int? id)
        {
            Session["articleId"] = id;
            var portPhoto = _db.Pictures.Include("Article").FirstOrDefault(i => i.ArticleId == id);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePhoto(IEnumerable<HttpPostedFileBase> image)
        {
            Picture createPhoto = new Picture();
            createPhoto.ArticleId = Convert.ToInt32(Session["articleId"]);

            foreach (var item in image)
            {
                createPhoto.Name = Path.GetFileName(item.FileName);
                createPhoto.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                item.SaveAs(createPhoto.ImageUrl);
                createPhoto.ImageUrl = createPhoto.Name;

                createPhoto.ArticleId = Convert.ToInt32(Session["articleId"]);
                _db.Pictures.Add(createPhoto);
                _db.SaveChanges();
            }
            return RedirectToAction("yonas", "Picture");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditPhoto(int id)
        {
            Picture editPhoto = new Picture();
            editPhoto.ArticleId = Convert.ToInt32(Session["portfoliId"]);
            using (_db = new ApplicationDbContext())
            {
                var articlePhotoEdit = _db.Pictures.Where(i => i.ArticleId == id).FirstOrDefault(i => i.Id == id);
                if (articlePhotoEdit != null)
                {
                    return View(articlePhotoEdit);
                }
                return RedirectToAction("yonas", "Picture");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhoto(IEnumerable<HttpPostedFileBase> image)
        {
            Picture editArticlePhoto = new Picture();
            editArticlePhoto.ArticleId = Convert.ToInt32(Session["portfoliId"]);

            foreach (var item in image)
            {
                editArticlePhoto.Name = Path.GetFileName(item.FileName);
                editArticlePhoto.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                item.SaveAs(editArticlePhoto.ImageUrl);
                editArticlePhoto.ImageUrl = editArticlePhoto.Name;

                _db.Pictures.Attach(editArticlePhoto);
                _db.Entry(editArticlePhoto).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("yonas", "Picture");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deleteArticle = _db.Article.Find(id);
                if (deleteArticle!=null)
                {
                    _db.Article.Remove(deleteArticle);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult TagDelete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteTag = _db.Tags.Find(id);
                if (deleteTag != null)
                {
                    _db.Tags.Remove(deleteTag);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}