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
        public ICollection<Student>? Applications {get;set;}

        public int Match(Preferences preferences) {
            int matchingRateExtreme = 0;
            if (this.Language == preferences.Language) matchingRateExtreme += 25;
            if (this.Location == preferences.Location) matchingRateExtreme += 25;
            if (preferences.Workdays.Contains(this.Meetingday)) matchingRateExtreme += 25;

            if (Keywords == null || Keywords.Count == 0 ) return matchingRateExtreme;
            int keyPoints = 25 / (preferences.Keywords.Count > 0 ? preferences.Keywords.Count : 1);
            foreach (var k in Keywords!)
            {   
                foreach (var key in preferences.Keywords)
                {
                    if (key.Str == k.Str) matchingRateExtreme += keyPoints;
                }
            }

            return matchingRateExtreme;
        }
    }
}