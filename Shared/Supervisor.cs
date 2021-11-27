namespace Shared
{

    public class Supervisor : ISupervisor
    {
        public ISet<Position>? Positions {get; set;}
        public ISet<IProject>? Projects {get; set;}
        public string? Name {get; set;}
        public int Id {get; set;}        
        public string? Email {get; set;}
        public DateTime DOB {get; set;}
        public University University {get; set;}
    }
}