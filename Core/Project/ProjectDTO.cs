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

        [Required]
        DateTime DueDate,

        [Required]
        int IntendedWorkHours,
        
        [Required]
        Language Language,

        
        ISet<string> Keywords,
        
        [Required]
        string SkillRequirementDescription,

        [Required]
        string SupervisorName,

        [Required]
        ISet<WorkDay> WorkDays,

        [Required]
        Location Location,

        [Required]
        bool isThesis
    );
}
