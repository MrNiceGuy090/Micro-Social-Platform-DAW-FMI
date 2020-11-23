using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MicroSocialPlatform.Models;

namespace MicroSocialPlatform.Controllers
{
    public class PostsController : Controller
    {
        private AppDBContext db = new AppDBContext();
        // GET: Posts
        public ActionResult Index()
        {
            var posts = from post in db.Posts
                        select post;
            ViewBag.Posts = posts;


            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            return View();
        }
        //Get
        public ActionResult Show(int id)
        {
            Post post = db.Posts.Find(id);
            ViewBag.Post = post;
            ViewBag.PostId = id;
            var comments = from comment in db.Comments
                           where comment.PostId == id
                           select comment;
            ViewBag.Comments = comments;
            return View(post);
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Post postare)
        {
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
        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Find(id);
            ViewBag.Post = post;
            return View(post);
        }
        [HttpPut]
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
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            TempData["message"] = "Postarea a fost sters!";
            return RedirectToAction("Index");
        }

    }
}