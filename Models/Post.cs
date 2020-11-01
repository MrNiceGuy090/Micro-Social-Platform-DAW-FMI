using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MicroSocialPlatform.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}