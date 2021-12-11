using Infrastructure;

namespace Infrastructure
{

    public class Student : IPerson
    {
        public DegreeEnum Degree {get; set;}
        public Preferences Preferences {get; set;} = new Preferences();
        public string? Name {get; set;}
        public int Id {get; set;}
        public string? Email {get; set;}
        public DateTime DOB {get; set;}        

        public UniversityEnum University {get; set;}
        public ICollection<Project> AppliedProjects {get; set;} = new List<Project>();
    }
}