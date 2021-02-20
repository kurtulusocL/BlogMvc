using Newtonsoft.Json;
using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using static ocLBlog.Web.Controllers.LearnPriceController.CaptchaResult;

namespace ocLBlog.Web.Controllers
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }

    public class HomeController : Controller
    {
        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            ApplicationDbContext veri = new ApplicationDbContext();

            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter xr = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xr.WriteStartDocument();
            xr.WriteStartElement("urlset");
            xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");

            xr.WriteStartElement("url");
            xr.WriteElementString("loc", "http://localhost:54383/");
            xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            xr.WriteElementString("changefreq", "daily");
            xr.WriteElementString("priority", "1");
            xr.WriteEndElement();

            var p = veri.Abouts;
            foreach (var b in p)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/About/" + b.Title);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/About/" + b.Subtitle);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/About/" + b.Description);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var pu = veri.Article;
            foreach (var b in pu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Article/" + b.Title);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Article/" + b.Subtitle);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Article/" + b.Subtext);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var puq = veri.Udemies;
            foreach (var b in puq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Udemy/" + b.Title);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Udemy/" + b.Subtitle);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Udemy/" + b.Detail);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var xu = veri.Categories;
            foreach (var b in xu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Category/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var qu = veri.Certificates;
            foreach (var b in qu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Certificate/" + b.Title);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Certificate/" + b.Subtitle);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Certificate/" + b.CertificateDate);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Certificate/" + b.FromWhere);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var zqu = veri.Portfolios;
            foreach (var b in zqu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Portfolio/" + b.Title);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Portfolio/" + b.Subtitle);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var xqu = veri.References;
            foreach (var b in xqu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Reference/" + b.Title);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Reference/" + b.Name);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Reference/" + b.Description);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var hq = veri.Tags;
            foreach (var b in hq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Tag/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var haq = veri.Pictures;
            foreach (var b in haq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/Picture/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var va = veri.SocialMedias;
            foreach (var b in va)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/SocialMedia/" + b.Facebook);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/SocialMedia/" + b.Instagram);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/SocialMedia/" + b.LinkedIn);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/SocialMedia/" + b.MailAddress);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/SocialMedia/" + b.GitHub);
                xr.WriteElementString("loc", "http://www.kurtulusocal.com/SocialMedia/" + b.Udemy);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            xr.WriteEndDocument();
            xr.Flush();
            xr.Close();
            Response.End();
            return View();
        }

        public ActionResult SocialMediaAbout()
        {
            using (_db = new ApplicationDbContext())
            {
                var socialMedia = _db.SocialMedias.Where(i => i.IsDeleted == false).ToList();
                return PartialView("_SocialMediaAbout", socialMedia);
            }
        }

        public ActionResult SocialMediaContact()
        {
            using (_db = new ApplicationDbContext())
            {
                var socialMedia = _db.SocialMedias.Where(i => i.IsDeleted == false).ToList();
                return PartialView("_SocialMediaContact", socialMedia);
            }
        }

        public ActionResult SkillAbout()
        {
            using (_db = new ApplicationDbContext())
            {
                var skill = _db.Skills.Where(i => i.IsDeleted == false).OrderByDescending(i => i.Degree).Take(7).ToList();
                return PartialView("_SkillAbout", skill);
            }
        }

        public ActionResult _CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateQuestion(Contact model)
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
                using (_db = new ApplicationDbContext())
                {
                    _db.Contacts.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    var result = _db.SaveChanges();

                    if (result > 0)
                    {
                        TempData["success"] = "I took to your message. Thank your message.";
                    }
                    else
                    {
                        TempData["error"] = "Please check to fro or your information. Because there was something wrong";
                    }
                }
            }
            return RedirectToAction("kurtulusocal", "About");
        }

        public ActionResult ArticlePhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var artPhoto = _db.Pictures.Include("Article").Where(i => i.IsDeleted == false && i.ArticleId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList();

                return PartialView("_ArticlePhoto", artPhoto);
            }
        }

        public ActionResult GetCategory(string title)
        {
            using (_db = new ApplicationDbContext())
            {
                var category = _db.Categories.Include("Articles").Where(i => i.IsDeleted == false && i.Articles.Count() > 0).OrderBy(i => i.Articles.Count()).ToList();

                return PartialView("_GetCategory", category);
            }
        }

        public ActionResult RandomArticle()
        {
            using (_db = new ApplicationDbContext())
            {
                var article = _db.Article.Include("Category").Include("Tag").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false).OrderByDescending(i => Guid.NewGuid()).Take(7).ToList();

                return PartialView("_RandomArticle", article);
            }
        }

        public ActionResult RandomArticlePhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var randomPhoto = _db.Pictures.Include("Article").Where(i => i.IsDeleted == false && i.ArticleId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList();

                return PartialView("_RandomArticlePhoto", randomPhoto);
            }
        }

        public ActionResult ArticleTag()
        {
            using (_db = new ApplicationDbContext())
            {
                var tagList = _db.Tags.Include("Articles").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(10).ToList();
                return PartialView("_ArticleTag", tagList);
            }
        }

        public ActionResult ArticleDetailPhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var detailPhoto = _db.Pictures.Include("Article").Where(i => i.IsDeleted == false && i.ArticleId == id).OrderByDescending(i => i.CreatedDate).ToList();
                return PartialView("_ArticleDetailPhoto", detailPhoto);
            }
        }

        public ActionResult HomeAbout()
        {
            using (_db=new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
                return PartialView("_HomeAbout", about);
            }
        }

        public ActionResult HomeSocialMedia()
        {
            using (_db=new ApplicationDbContext())
            {
                var social = _db.SocialMedias.Where(i => i.IsDeleted == false).ToList();
                return PartialView("_HomeSocialMedia", social);
            }
        }

        public ActionResult HomeArticles()
        {
            using (_db=new ApplicationDbContext())
            {
                var articles = _db.Article.Include("Tag").Include("Category").Include("Comments").Include("Pictures").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(6).ToList();

                return PartialView("_HomeArticles", articles);
            }
        }

        public ActionResult HomeArticlePhoto(int? id)
        {
            using (_db=new ApplicationDbContext())
            {
                var articlePhoto = _db.Pictures.Include("Article").Where(i => i.IsDeleted == false && i.ArticleId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList();
                return PartialView("_HomeArticlePhoto", articlePhoto);
            }
        }

        public ActionResult HomePortfolio()
        {
            using (_db=new ApplicationDbContext())
            {
                var portfolio = _db.Portfolios.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(6).ToList();
                return PartialView("_HomePortfolio", portfolio);
            }
        }

        public ActionResult HomeCertificate()
        {
            using (_db=new ApplicationDbContext())
            {
                var certificate = _db.Certificates.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(6).ToList();
                return PartialView("_HomeCertificate", certificate);
            }
        }

        public ActionResult _HomeReference()
        {
            return View();
        }

        public ActionResult HomeReferenceRight()
        {
            using (_db = new ApplicationDbContext())
            {
                var reference = _db.References.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(5).ToList();
                return PartialView("_HomeReferenceRight", reference);
            }
        }

        public ActionResult HomeSkill()
        {
            using (_db=new ApplicationDbContext())
            {
                var skill = _db.Skills.Where(i => i.IsDeleted == false).OrderByDescending(i => i.Degree).Take(7).ToList();
                return PartialView("_HomeSkill", skill);
            }
        }

        public ActionResult Slider()
        {
            using (_db=new ApplicationDbContext())
            {
                var slider = _db.Portfolios.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(9).ToList();
                return PartialView("_Slider", slider);
            }
        }

        public ActionResult AboutShared()
        {
            using (_db=new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).ToList();
                return PartialView("_AboutShared", about);
            }
        }
    }
}