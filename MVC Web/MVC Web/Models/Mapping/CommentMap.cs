using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Web.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NameSurname)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Text)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("Comment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NameSurname).HasColumnName("NameSurname");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ArticleID).HasColumnName("ArticleID");
            this.Property(t => t.CommentID).HasColumnName("CommentID");
            this.Property(t => t.AddedDate).HasColumnName("AddedDate");
            this.Property(t => t.isCheck).HasColumnName("isCheck");

            // Relationships
            this.HasOptional(t => t.Article)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.ArticleID);
            this.HasOptional(t => t.Comment2)
                .WithMany(t => t.Comment1)
                .HasForeignKey(d => d.CommentID);

        }
    }
}
