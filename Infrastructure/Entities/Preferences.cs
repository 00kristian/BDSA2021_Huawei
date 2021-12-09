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
        public LocationEnum Location {get;set;}
        public ICollection<Keyword> Keywords {get;set;} = new List<Keyword>();
    }
}