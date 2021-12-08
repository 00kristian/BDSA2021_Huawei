using System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core{

    public record struct StudentDTO(
        [Required]
        DegreeEnum Degree,

        [Required]
        int PreferenceId,

        [Required, StringLength(30)]
        string Name,

        int Id,

        [Required, EmailAddress]
        string Email,

        [Required]
        DateTime DOB,   

        UniversityEnum University,

        ICollection<int> AppliedProjects
    );
}
    
