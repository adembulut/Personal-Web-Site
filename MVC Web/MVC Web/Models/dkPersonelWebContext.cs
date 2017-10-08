using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MVC_Web.Models.Mapping;

namespace MVC_Web.Models
{
    public partial class dkPersonelWebContext : DbContext
    {
        static dkPersonelWebContext()
        {
            Database.SetInitializer<dkPersonelWebContext>(new Configuration());
        }

        public dkPersonelWebContext()
            : base("Name=dkPersonelWebContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new WorkMap());
        }
    }
}
