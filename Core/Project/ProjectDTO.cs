using System.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorApp.Core;
using Core;

namespace Core{

    public record struct ProjectDTO(
        [Required, StringLength(30)]
        string Name,

        [Required]
        int Id,

        [Required]
        string Description,

        DateTime DueDate,

        int IntendedWorkHours,
        
        [Required]
        LanguageEnum Language,
        
        string SkillRequirementDescription,

        [Required]
        string SupervisorName,

        string Location,

        [Required]
        bool isThesis,

        [Required]
        ICollection<string> Keywords 
        );
}
