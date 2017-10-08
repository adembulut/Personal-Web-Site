using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC_Web.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Username)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.Gsm)
                .HasMaxLength(11);

            this.Property(t => t.Mail)
                .HasMaxLength(50);

            this.Property(t => t.WebSite)
                .HasMaxLength(50);

            this.Property(t => t.NameSurname)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.isAdmin).HasColumnName("isAdmin");
            this.Property(t => t.Gsm).HasColumnName("Gsm");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.WebSite).HasColumnName("WebSite");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.NameSurname).HasColumnName("NameSurname");
        }
    }
}
