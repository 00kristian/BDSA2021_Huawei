namespace Shared{
    public interface IProject
    {
        //Properties 
        string Name{get;set;}
        int Id{get;set;}
        string Description{get;set;}
        DateTime DueDate{get;set;}
        int IntendedWorkHours{get;set;}
        Language Language{get;set;}
        ICollection<Keyword> Keywords{get;set;}
        string SkillRequirementDescription{get;set;}
        ISupervisor Supervisor{get;set;}
        ICollection<WorkDay> WorkDays{get;set;}
        ICollection<Location> Locations{get;set;}
        bool isThesis{get;set;}
    }
    //TODO: eventuel sætte annotations på  
}