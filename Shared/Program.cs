using System;
using Microsoft.EntityFrameworkCore;

namespace Shared {
internal class Program
{
    private static void Main()
    {
        
        var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        
        using (var db = new ProjectBankContext(builder.Options))
        {
            Console.WriteLine($"Database path: {db.DbPath}.");
            
            /*var PR = new ProjectRepository(db);

            PR.DELETE_ALL_PROJECTS_TEMPORARY();

            string inputName;
            while ((inputName = Console.ReadLine()) != null) {
                System.Console.WriteLine("Input project name to be created");
                //var ID = PR.Create(inputName);
                //System.Console.WriteLine(inputName + " created, ID: " + ID + "\n");
                System.Console.WriteLine("All Projects in database: ");
                foreach (var name in PR.ReadAllNames())
                {
                    System.Console.WriteLine(name);
                }
            }*/
        }
    }
}
}