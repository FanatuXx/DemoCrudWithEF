using DemoCrudWithEF.Domain.Commands;
using DemoCrudWithEF.Domain.Entities;
using DemoCrudWithEF.Domain.Errors;
using DemoCrudWithEF.Domain.Queries;
using DemoCrudWithEF.Domain.Repositories;
using Tools.Results;

namespace DemoCrudWithEF.Domain.Services
{
    public class GroupeService : IGroupeRepository
    {
        private readonly MusicDbContext _dbContext;

        public GroupeService(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result Handle(AddGroupeCommand command)
        {
            try
            {
                Groupe groupe = new Groupe() { Nom = command.Nom };
                _dbContext.Add(groupe);
                _dbContext.SaveChanges();
                return Result.Success();
            }
            catch (Exception)
            {
                return GroupeErrors.GroupeException;
            }
        }

        public Result<IEnumerable<Groupe>> Handle(GetGroupesQuery query)
        {
            return Result<IEnumerable<Groupe>>.Success(_dbContext.Groupes.AsEnumerable());
        }

        public async Task<Result<Groupe>> HandleAsync(GetGroupeByIdQuery query)
        {
            Groupe? groupe = await _dbContext.Groupes.FindAsync(query.Id);

            if (groupe is null)
                return GroupeErrors.GroupeNotFound;

            if (query.WithAlbums)
                await _dbContext.Entry(groupe).Collection(g => g.Albums).LoadAsync();

            return Result<Groupe>.Success(groupe);
        }

        public Result Handle(UpdateGroupeCommand command)
        {
            Groupe? groupe = _dbContext.Groupes.Find(command.Id);

            if (groupe is null)
                return GroupeErrors.GroupeNotFound;

            groupe.Nom = command.Nom;
            _dbContext.SaveChanges();
            return Result.Success();
        }
    }
}
