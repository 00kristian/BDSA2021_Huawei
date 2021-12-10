using System.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;

namespace Core{

    public record struct ProjectDTO(
        [Required, StringLength(30)]
        string Name,

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

        LocationEnum Location,

        [Required]
        bool IsThesis,

        [Required]
        ICollection<string> Keywords,

        WorkdayEnum Meetingday,

        int Rating
    );
}
