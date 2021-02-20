using Microsoft.AspNet.Identity.EntityFramework;
using ocLBlog.Data.Identity;
using ocLBlog.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocLBlog.Data.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext():base("DefaultConnection")
        {

        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LearnPrice> LearnPrices { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Udemy> Udemies { get; set; }
    }
}
