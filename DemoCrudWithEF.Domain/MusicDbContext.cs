using DemoCrudWithEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoCrudWithEF.Domain
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Album> Albums { get { return Set<Album>(); } }
        public DbSet<Groupe> Groupes { get { return Set<Groupe>(); } }

        public MusicDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicDbContext).Assembly);
        }
    }
}
