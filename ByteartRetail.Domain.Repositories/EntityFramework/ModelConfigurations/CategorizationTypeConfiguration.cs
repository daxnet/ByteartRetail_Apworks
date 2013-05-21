using ByteartRetail.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ByteartRetail.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class CategorizationTypeConfiguration : EntityTypeConfiguration<Categorization>
    {
        public CategorizationTypeConfiguration()
        {
            HasKey(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ProductID)
                .IsRequired();
            Property(c => c.CategoryID)
                .IsRequired();
        }
    }
}
