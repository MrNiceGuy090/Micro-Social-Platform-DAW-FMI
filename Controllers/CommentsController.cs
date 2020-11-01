using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MicroSocialPlatform.Models;

namespace MicroSocialPlatform.Controllers
{
    public class CommentsController : Controller
    {
        private AppDBContext db = new AppDBContext();
        // GET: Comments
        [HttpPost]
        public ActionResult New(Comment comment)
        {
            var postId = comment.PostId;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Show", "Posts", new { id = postId });
        }

        public ActionResult Edit(int id)
        {
            Comment com = db.Comments.Find(id);
            ViewBag.Comment = com;
            return View();
        }
        [HttpPut]
        public ActionResult Edit(int id, Post requestCom)
        {
            try
            {
                Comment com = db.Comments.Find(id);
                if (TryUpdateModel(com))
                {
                    com.Content = requestCom.Content;
                    com.Date = requestCom.Date;
                    db.SaveChanges();
                }
                return RedirectToAction("Show","Posts", new { id = com.PostId } );
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Comment com = db.Comments.Find(id);
            var postId = com.PostId;
            db.Comments.Remove(com);
            db.SaveChanges();
            return RedirectToAction("Show","Posts", new {id=postId });
        }
    }
}