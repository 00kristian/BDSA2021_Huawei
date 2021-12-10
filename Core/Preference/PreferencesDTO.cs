using System.ComponentModel.DataAnnotations;
using Core;

namespace Core{

    public record struct PreferencesDTO(
        [Required]
        LanguageEnum Language,
        
        ICollection<string> Keywords,

        [Required]
        ICollection<WorkdayEnum> Workdays,

        [Required]
        LocationEnum Location
    );
}
