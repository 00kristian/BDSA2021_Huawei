namespace Shared{
    public interface ISupervisor: IPerson
    {
        //Properties 
        ISet<Position> Positions{get;set;}
        ISet<IProject> Projects{get;set;}
        
    }
    //TODO: eventuel sætte annotations på  
}