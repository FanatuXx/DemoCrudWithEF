using System;
using System.Collections.Generic;
using System.Text;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Commands
{
    public record UpdateGroupeCommand(int Id, string Nom) : ICommandDefinition;    
}
