namespace Shared{
    public interface IStudent: IPerson
    {
        //Properties 
        //Degree Degree{get;set;}
        
        //IPreferences? Preferences{get; set;}

        void ApplyForProject(IProject p);
        
        //projectMatchPercent(Project)

    }
    //TODO: eventuel sætte annotations på  
}