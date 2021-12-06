using System.ComponentModel.DataAnnotations;
using Core;

namespace BlazorApp.Core{

    public record struct PreferenceDTO(
        [Required]
        LanguageEnum Language,
        
        ISet<KeywordEnum> Keywords,

        [Required]
        WorkDayEnum WorkDays,

        [Required]
        LocationEnum Locations
    );
}
