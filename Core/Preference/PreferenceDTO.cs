using System.ComponentModel.DataAnnotations;
using Core;

namespace BlazorApp.Core{

    public record struct PreferenceDTO(
        [Required]
        Language Language,
        
        ISet<KeywordEnum> Keywords,

        [Required]
        ISet<WorkDay> WorkDays,

        [Required]
        ISet<Location> Locations
    );
}
