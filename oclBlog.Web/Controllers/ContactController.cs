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

    public class ContactController : Controller
    {
        ApplicationDbContext _db;

        public ContactController()
        {
            _db = new ApplicationDbContext();
        }

        [Route("~/contact/me")]
        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var contact = _db.Contacts.Where(i => i.IsDeleted == false).ToList();
                return View(contact);
            }
        }

        public ActionResult _Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(Contact model)
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

            using (_db = new ApplicationDbContext())
            {
                _db.Contacts.Add(model);
                _db.Entry(model).State = EntityState.Added;
                var result = _db.SaveChanges();
                if (result > 0)
                {
                    TempData["success"] = "I took to your message. Thank you for message.";
                }
                else
                {
                    TempData["error"] = "İletiniz gönderilemedi. Lütfen kontrol ederek tekrar deneyiniz.";
                }
                return RedirectToAction("me");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var contact = _db.Contacts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(contact);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ContactDetail(int id)
        {
            return View(_db.Contacts.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteContact = _db.Contacts.Find(id);
                if (deleteContact != null)
                {
                    _db.Contacts.Remove(deleteContact);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}