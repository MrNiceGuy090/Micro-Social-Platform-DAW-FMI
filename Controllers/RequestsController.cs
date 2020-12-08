﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroSocialPlatform.Models;
using Microsoft.AspNet.Identity;

namespace MicroSocialPlatform.Controllers
{
    public class RequestsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Requests
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Send(string id)
        {
            Request req = new Request();
            req.Sent = User.Identity.GetUserId();
            req.Received = id;
            db.Request.Add(req);
            db.SaveChanges();
            return RedirectToAction("Index", "Profile", new { id });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Accept(string id)
        {
            Request req = db.Request.Find(id, User.Identity.GetUserId());
            if (req != null)
            {
                Friendship friends1 = new Friendship();
                friends1.Id = User.Identity.GetUserId();
                friends1.Id2 = id;

                Friendship friends2 = new Friendship();
                friends2.Id = id;
                friends2.Id2 = User.Identity.GetUserId();

                db.Friendship.Add(friends1);
                db.Friendship.Add(friends2);
                db.Request.Remove(req);

                db.SaveChanges();
                return RedirectToAction("Index", "Profile", new { id });
            }
            return View();
        }


    }
}