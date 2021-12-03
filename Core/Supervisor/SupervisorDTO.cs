using System.ComponentModel.DataAnnotations;
using Core;

namespace BlazorApp.Core{

    public record struct SupervisorDTO(

        Position Position,

        [Required, StringLength(30)]
        string Name,

        [Required]
        int Id,

        [Required]
        DateTime DOB,
        
        University University
        
        
    );
}
