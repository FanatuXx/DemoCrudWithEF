using DemoCrudWithEF.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Results;

namespace DemoCrudWithEF.Domain.Errors
{
    public static class GroupeErrors
    {
        public static Error GroupeException => Error.Create("Groupe.Exception", "Une exception est survenue."); 
    }
}
