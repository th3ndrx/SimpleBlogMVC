using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlogMVC.Models;

namespace SimpleBlogMVC.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("ApplicationDbContext")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Add your future DbSets here
        public DbSet<Article> Articles { get; set; }
    }
}