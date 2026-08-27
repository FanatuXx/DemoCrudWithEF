using System.ComponentModel.DataAnnotations;

namespace DemoCrudWithEF.Models
{
    public class EditGroupeForm
    {
        [Required]
        [StringLength(130, MinimumLength = 1)]
        public string Nom { get; set; }
    }
}
