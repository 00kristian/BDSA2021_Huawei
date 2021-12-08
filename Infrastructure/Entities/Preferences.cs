using Infrastructure;
using Core;

namespace Infrastructure
{
    public class Preferences 
    {
        public int Id {get;set;}
        public Student? Student {get; set;}
        public int StudentId {get; set;}
        public LanguageEnum Language {get;set;}
        public ICollection<WorkdayEnum> Workdays {get;set;} = new List<WorkdayEnum>();
        public ICollection<LocationEnum> Locations {get;set;} = new List<LocationEnum>();
        public ICollection<Keyword>? Keywords {get;set;}
    }
}