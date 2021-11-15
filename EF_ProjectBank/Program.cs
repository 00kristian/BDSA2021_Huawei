using System;
using System.Linq;

namespace EF_PB;
internal class Program
{
    private static void Main()
    {
        using (var db = new ProjectBankContext())
        {
            Console.WriteLine($"Database path: {db.DbPath}.");
            
            // Create
            Console.WriteLine("Inserting a new project");
            db.projects.Add(new Project{Name = "p1"});
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for a project");
            var p = db.projects
                .First();
            System.Console.WriteLine(p.Name);
        }
    }
}
