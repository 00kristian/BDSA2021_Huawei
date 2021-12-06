using Infrastructure;
using Core;

namespace Infrastructure
{
    public class Preferences 
    {
        public int Id {get;set;}
        public string? Language {get;set;}
        public ISet<Workday>? Workdays {get;set;}
        public ISet<Location>? Locations {get;set;}
        public ISet<Keyword>? Keywords {get;set;}
    }
}