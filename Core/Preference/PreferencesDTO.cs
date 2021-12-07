using System.ComponentModel.DataAnnotations;
using Core;

namespace Core{

    public record struct PreferencesDTO(
        [Required]
        LanguageEnum Language,
        
        List<string> Keywords,

        [Required]
        List<WorkdayEnum> WorkDays,

        [Required]
        List<LocationEnum> Locations
    );
}
