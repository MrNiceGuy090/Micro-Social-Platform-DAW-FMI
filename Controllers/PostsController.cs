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
            return View();
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Post postare)
        {
            db.Posts.Add(postare);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }
        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Find(id);
            ViewBag.Post = post;
            return View();
        }
        [HttpPut]
        public ActionResult Edit(int id, Post requestPost)
        {
            try
            {
                Post post = db.Posts.Find(id);
                if (TryUpdateModel(post))
                {
                    post.Content = requestPost.Content;
                    post.Date = requestPost.Date;
                    db.SaveChanges();
                }
                return RedirectToAction("Show",new { id=id });
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
            return RedirectToAction("Index");
        }

    }
}