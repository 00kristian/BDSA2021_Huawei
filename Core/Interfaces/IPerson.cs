namespace Shared{
    public interface IPerson
    {
        //Properties 
        string Name { get; set;}
        int Id { get; set; }
        string Email { get; set; }
        DateTime DOB {get;set;}
        UniversityEnum University{get;set;}

    }
    //TODO: eventuel sætte annotations på  
}