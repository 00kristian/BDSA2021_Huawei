using System.ComponentModel.DataAnnotations;
using Core;

namespace BlazorApp.Core{

    public record struct PreferenceDTO(
        [Required]
        string Language,
        
        ISet<KeywordEnum> Keywords,

        [Required]
        ISet<WorkDayEnum> WorkDays,

        [Required]
        ISet<LocationEnum> Locations
    );
}
