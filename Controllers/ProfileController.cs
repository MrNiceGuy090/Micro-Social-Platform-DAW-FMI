using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroSocialPlatform.Models;
using Microsoft.AspNet.Identity;

namespace MicroSocialPlatform.Controllers
{
    public class ProfileController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Profile
        public ActionResult Index(String id)
        {
            var profil = db.Profile.Find(id);
            ViewBag.User = profil;

            var posts = db.Posts.Where(p => p.UserId == profil.Id).OrderByDescending(p => p.Date).ToList();
            ViewBag.Posts = posts;
            return View();
        }
    }
}