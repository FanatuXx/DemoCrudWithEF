using DemoCrudWithEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCrudWithEF.Domain.Configurations
{
    public class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Album", t => {
                t.HasCheckConstraint("CK_Album_Titre", "LEN(TRIM(Titre)) > 0");
                t.HasCheckConstraint("CK_Album_Annee", "Annee >= 1888");
            });

            builder.Property(a => a.Titre)
                .IsRequired()
                .HasColumnType("NVARCHAR(130)");

            builder.Property(a => a.Annee)
                .IsRequired();

            builder.HasOne(a => a.Groupe)
                .WithMany(g => g.Albums)
                .HasForeignKey(a => a.GroupeId);
        }
    }
}
