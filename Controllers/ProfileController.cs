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

            // Este prieten sau a primit request
            if (db.Friendship.Find(id, User.Identity.GetUserId()) != null)
                ViewBag.Friendship = "prieteni";
            else if (db.Request.Find(User.Identity.GetUserId(),id) != null)
                ViewBag.Friendship = "trimisa";
            else if (db.Request.Find(id, User.Identity.GetUserId()) != null)
                ViewBag.Friendship = "primita";
            else if (id != User.Identity.GetUserId())
                ViewBag.Friendship = "straini";


            var friendsid = db.Friendship.Where(p => p.Id == profil.Id).ToList();
            List<Profile> friends = new List<Profile>();
            foreach (var i in friendsid)
            {
                friends.Add(db.Profile.Find(i.Id2));
            }

            ViewBag.NoFriends = friends.Count();
            ViewBag.Friends = friends;
            return View();
        }
    }
}