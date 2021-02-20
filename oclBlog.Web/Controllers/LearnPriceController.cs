using Newtonsoft.Json;
using ocLBlog.Data.Data;
using ocLBlog.Entities.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static ocLBlog.Web.Controllers.CaptchaResult;

namespace ocLBlog.Web.Controllers
{
    public class LearnPriceController : Controller
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

        public LearnPriceController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/price/orderproject")]
        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var price = _db.LearnPrices.Where(i => i.IsDeleted == false).ToList();
                return View();
            }
        }

        public ActionResult _Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(LearnPrice model)
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
                    _db.LearnPrices.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    var result = _db.SaveChanges();

                    if (result > 0)
                    {
                        TempData["success"] = "İletiniz başarıyla gönderilmiştir.";
                    }
                    else
                    {
                        TempData["error"] = "İletiniz gönderilemedi. Lütfen kontrol ederek tekrar deneyiniz.";
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var price = _db.LearnPrices.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 10);
                return View(price);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult LearnPriceDetail(int id)
        {
            return View(_db.LearnPrices.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deletePrice = _db.LearnPrices.Find(id);
                if (deletePrice != null)
                {
                    _db.LearnPrices.Remove(deletePrice);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}