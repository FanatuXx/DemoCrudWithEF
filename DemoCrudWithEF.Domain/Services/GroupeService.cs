using DemoCrudWithEF.Domain.Commands;
using DemoCrudWithEF.Domain.Entities;
using DemoCrudWithEF.Domain.Errors;
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
    }
}
