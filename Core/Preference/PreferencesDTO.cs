using System.ComponentModel.DataAnnotations;
using Core;

namespace Core{

    public record struct PreferencesDTO(
        [Required]
        string Language,
        
        List<string> Keywords,

        [Required]
        List<string> WorkDays,

        [Required]
        List<string> Locations
    );
}
