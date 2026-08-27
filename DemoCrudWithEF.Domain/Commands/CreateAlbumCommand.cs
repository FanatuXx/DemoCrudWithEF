using System;
using System.Collections.Generic;
using System.Text;
using Tools.CommandQuerySeparation;

namespace DemoCrudWithEF.Domain.Commands
{
    public record CreateAlbumCommand(string Titre, int Annee, int GroupeId) : ICommandDefinition;
}
