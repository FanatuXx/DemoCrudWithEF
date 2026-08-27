using DemoCrudWithEF.Domain.Commands;
using DemoCrudWithEF.Domain.Entities;
using DemoCrudWithEF.Domain.Queries;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Repositories
{
    public interface IGroupeRepository :
        IQueryHandler<GetGroupesQuery, IEnumerable<Groupe>>,
        IQueryAsyncHandler<GetGroupeByIdQuery, Groupe>,
        ICommandHandler<AddGroupeCommand>,
        ICommandHandler<UpdateGroupeCommand>
    {
    }
}
