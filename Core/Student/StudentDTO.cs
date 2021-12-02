using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core{

    public record struct StudentDTO(
        [Required]
        Degree Degree,

        //[Required]
        //IPreferences Preference,

        [Required, StringLength(30)]
        string Name,

        [Required]
        int Id,

        [Required, EmailAddress]
        string Email,

        [Required]
        DateTime DOB,   

        [Required]
        University University
    );
}
