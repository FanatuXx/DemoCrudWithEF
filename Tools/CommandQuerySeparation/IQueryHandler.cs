using System;
using System.Collections.Generic;
using System.Text;
using Tools.Results;

namespace Tools.CommandQuerySeparation
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQueryDefinition<TResult>
    {
        Result<TResult> Handle(TQuery query);
    }

    public interface IQueryAsyncHandler<TQuery, TResult>
        where TQuery : IQueryDefinition<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query);
    }
}
