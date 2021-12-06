using Infrastructure;

namespace Infrastructure
{

    public class Student : IPerson
    {
        public Degree Degree {get; set;}
        public int PreferenceId {get; set;}

        public string? Name {get; set;}
        public int Id {get; set;}
        public string? Email {get; set;}
        public DateTime DOB {get; set;}        

        public University University {get; set;}
        public ICollection<Project> AppliedProjects {get; set;}


        public void ApplyForProject(Project p)
        {
            throw new NotImplementedException();
        }
    }
}