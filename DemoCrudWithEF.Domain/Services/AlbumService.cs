using DemoCrudWithEF.Domain.Commands;
using DemoCrudWithEF.Domain.Entities;
using DemoCrudWithEF.Domain.Errors;
using DemoCrudWithEF.Domain.Repositories;
using Tools.Results;

namespace DemoCrudWithEF.Domain.Services
{
    public class AlbumService : IAlbumRepository
    {
        private readonly MusicDbContext _dbContext;

        public AlbumService(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> HandleAsync(CreateAlbumCommand command, CancellationToken cancellationToken)
        {
            Groupe? groupe = await _dbContext.Groupes.FindAsync(command.GroupeId);

            if (groupe is null)
                return GroupeErrors.GroupeNotFound;

            groupe.Albums.Add(new Album() { Titre = command.Titre, Annee = command.Annee });
            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
