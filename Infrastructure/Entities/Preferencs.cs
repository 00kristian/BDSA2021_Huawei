using Infrastructure;

namespace Core
{
    public class Preferences : IPreferences
    {
        public ISet<Language>? Languages {get;set;}
        public ISet<WorkDay>? WorkDays {get;set;}
        public ISet<Location>? Locations {get;set;}
        public ISet<Keyword>? Keywords {get;set;}
    }
}