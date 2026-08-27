using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoCrudWithEF.Models
{
    public class AddAlbumForm
    {
        [Required]
        [StringLength(130, MinimumLength = 1)]
        public string Titre { get; set; } = default!;
        [Required]
        [Range(1888, 9999)]
        public int Annee { get; set; } = 1968;

        [HiddenInput]
        public int GroupeId { get; set; }
    }
}
