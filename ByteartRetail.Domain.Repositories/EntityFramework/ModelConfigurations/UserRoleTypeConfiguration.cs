using ByteartRetail.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ByteartRetail.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class UserRoleTypeConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleTypeConfiguration()
        {
            HasKey(ur => ur.ID);
            Property(ur => ur.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(ur => ur.RoleID)
                .IsRequired();
            Property(ur => ur.UserID)
                .IsRequired();
        }
    }
}
