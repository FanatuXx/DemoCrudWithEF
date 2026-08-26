using DemoCrudWithEF.Domain.Commands;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Repositories
{
    public interface IGroupeRepository :
        ICommandHandler<AddGroupeCommand>
    {
    }
}
