using DemoCrudWithEF.Domain.Commands;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Repositories
{
    public interface IAlbumRepository : 
        ICommandAsyncHandler<CreateAlbumCommand>
    {
    }
}
