namespace Shared
{

    public class Student : IStudent
    {
        public Degree Degree {get; set;}
        public IPreferences Preferences {get; set;}
        public string Name {get; set;}
        public int Id {get; set;}
        public string Email {get; set;}
        public DateTime DOB {get; set;}        
        public University University {get; set;}
        public void ApplyForProject(IProject p)
        {
            throw new NotImplementedException();
        }
    }
}