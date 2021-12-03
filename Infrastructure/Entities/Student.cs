using Infrastructure;

namespace Infrastructure
{

    public class Student : IPerson
    {
        public Degree Degree {get; set;}
        public Preferences? Preferences {get; set;}
        public string? Name {get; set;}
        public int Id {get; set;}
        public string? Email {get; set;}
        public DateTime DOB {get; set;}        
        public University University {get; set;}
        public void ApplyForProject(Project p)
        {
            throw new NotImplementedException();
        }
    }
}