using Infrastructure;

namespace Infrastructure
{

    public class Supervisor : ISupervisor
    {
        
        public Position? Position { get; set;}
        public string? Name {get; set;}
        public int Id {get; set;}        
        public string? Email {get; set;}
        public DateTime DOB {get; set;}
        public University University {get; set;}
    }
}