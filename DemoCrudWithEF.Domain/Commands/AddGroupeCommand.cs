using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Commands
{
    public record AddGroupeCommand(string Nom) : ICommandDefinition;
}
