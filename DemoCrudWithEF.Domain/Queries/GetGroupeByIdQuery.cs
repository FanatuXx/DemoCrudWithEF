using DemoCrudWithEF.Domain.Entities;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Queries
{
    public record GetGroupeByIdQuery(int Id, bool WithAlbums = false) : IQueryDefinition<Groupe>;
}
