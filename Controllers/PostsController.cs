﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MicroSocialPlatform.Models;
using Microsoft.AspNet.Identity;

namespace MicroSocialPlatform.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Posts

        [Authorize(Roles = "User,Admin")]
        public ActionResult Index()
        {
            //var posts = from post in db.Posts
            //            select post;
            var posts = db.Posts.Include("User");
            ViewBag.Posts = posts;


            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            return View();
        }
        //Get
        [Authorize(Roles = "User,Admin")]
        public ActionResult Show(int id)
        {
            Post post = db.Posts.Find(id);

            SetAccessRights();

            ViewBag.Post = post;
            ViewBag.PostId = id;
            var comments = from comment in db.Comments
                           where comment.PostId == id
                           select comment;
            ViewBag.Comments = comments;
            return View(post);
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult New()
        {
            Post post = new Post();
            post.UserId = User.Identity.GetUserId();
            return View(post);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult New(Post postare)
        {
            postare.UserId = User.Identity.GetUserId();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Posts.Add(postare);
                    db.SaveChanges();
                    TempData["message"] = "Postarea a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(postare);
                }
            }
            catch (Exception e)
            {
                return View(postare);
            }

        }
        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Find(id);
            ViewBag.Post = post;

            if (post.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                return View(post);
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui postari care nu va apartine";
                return RedirectToAction("Index");
            }
        }
        [HttpPut]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Edit(int id, Post requestPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Post post = db.Posts.Find(id);

                    if (TryUpdateModel(post))
                    {
                        post.Content = requestPost.Content;
                        post.Date = requestPost.Date;
                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("Show", new { id = id });
                }

                else
                {
                    return View(requestPost);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);

            if (post.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                TempData["message"] = "Postarea a fost sters!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti unei postari care nu va apartine";
                return RedirectToAction("Index");
            }
        }

        private void SetAccessRights()
        {
            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Admin"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Admin");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
        }


    }
}