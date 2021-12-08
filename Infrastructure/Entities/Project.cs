using System.ComponentModel.DataAnnotations.Schema;
using Core;
using Infrastructure;

namespace Infrastructure
{

    public class Project
    {
        public string? Name {get; set;}
        public int Id {get;set;}
        public string? Description { get; set; }       
        public DateTime DueDate { get; set; }
        public int IntendedWorkHours { get; set; }
        public LanguageEnum Language { get; set; }               
        public string? SkillRequirementDescription { get; set; }
        public string? SupervisorName { get; set; }
        public LocationEnum Location { get; set; }        
        public bool IsThesis { get; set; }
        public ICollection<Keyword>? Keywords {get;set;}
        public WorkdayEnum Meetingday {get;set;}
    }
}