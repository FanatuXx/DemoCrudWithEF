using DemoCrudWithEF.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Queries
{
    public record GetGroupesQuery : IQueryDefinition<IEnumerable<Groupe>>;
}
