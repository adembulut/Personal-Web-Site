using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Web.Models.Mapping
{
    public class WorkMap : EntityTypeConfiguration<Work>
    {
        public WorkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Header)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Work");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Header).HasColumnName("Header");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.AddedDate).HasColumnName("AddedDate");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.Works)
                .HasForeignKey(d => d.UserID);

        }
    }
}
