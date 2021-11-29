using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{

    public class Project : IProject
    {
        public string? Name {get; set;}
        public int Id {get;set;}
        public string? Description { get; set; }       
        public DateTime DueDate { get; set; }
        public int IntendedWorkHours { get; set; }
        //public Language Language { get; set; }        
       
        //public ISet<KeywordEnum>? Keywords { get; set; }
        public string? SkillRequirementDescription { get; set; }
        
        //public ISupervisor? Supervisor { get; set; }
        
        //public ISet<WorkDay>? WorkDays { get; set; }
        
        //public ISet<Location>? Locations { get; set; }        
        public bool isThesis { get; set; }

    }
}