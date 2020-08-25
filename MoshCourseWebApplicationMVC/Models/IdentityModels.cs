using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MoshCourseWebApplicationMVC.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>().HasRequired(a => a.Gig).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ApplicationUser>().
                HasMany(u => u.Followers).
                WithRequired(f => f.Followee).
                WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>().
                HasMany(u => u.Followees).
                WithRequired(f => f.Follower).
                WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}