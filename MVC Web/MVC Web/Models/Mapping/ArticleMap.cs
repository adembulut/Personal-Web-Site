using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Web.Models.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Header)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Article");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Header).HasColumnName("Header");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.AddedDate).HasColumnName("AddedDate");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.Articles)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Articles)
                .HasForeignKey(d => d.UserID);

        }
    }
}
