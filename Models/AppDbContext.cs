using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MicroSocialPlatform.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("DBConnectionString") {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDBContext, MicroSocialPlatform.Migrations.Configuration>("DBConnectionString"));
        
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}