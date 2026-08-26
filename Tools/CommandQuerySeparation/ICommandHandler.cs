using System;
using System.Collections.Generic;
using System.Text;
using Tools.Results;

namespace Tools.CommandQuerySeparation
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommandDefinition
    {
        Result Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommandDefinition<TResult>
    {
        Result<TResult> Handle(TCommand command);
    }
}
