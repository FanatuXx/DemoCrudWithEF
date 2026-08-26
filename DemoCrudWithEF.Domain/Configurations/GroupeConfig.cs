using DemoCrudWithEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCrudWithEF.Domain.Configurations
{
    internal class GroupeConfig : IEntityTypeConfiguration<Groupe>
    {
        public void Configure(EntityTypeBuilder<Groupe> builder)
        {
            builder.ToTable("Groupe");

            builder.Property(g => g.Nom)
                .IsRequired()
                .HasColumnType("NVARCHAR(75)");
        }
    }
}
