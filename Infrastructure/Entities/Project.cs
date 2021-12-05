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
        //public Language Language { get; set; }        
       
        //public ISet<string>? Keywords { get; set; }
        public string? SkillRequirementDescription { get; set; }
        
        public string? SupervisorName { get; set; }
        
        //public ISet<string>? WorkDays { get; set; }
        
        //public Location Location { get; set; }        
        public bool isThesis { get; set; }
    }
}