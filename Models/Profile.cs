using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroSocialPlatform.Models
{
    public class Profile
    {
        [Key]
        public String Id { get; set;}
        public String Name { get; set; }
    }
}