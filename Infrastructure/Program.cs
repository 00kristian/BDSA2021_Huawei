// See https://aka.ms/new-console-template for more information
using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        
    using (var db = new ProjectBankContextFactory().CreateDbContext(new string[0]))
    {
        var repo = new StudentRepository(db);

        while (true)
        {
            Console.WriteLine("Enter name:");
            string line = Console.ReadLine()!;
            if (line == "exit")
            {
                break;
            }
            var res = await repo.Create(new StudentDTO{
                Name = line,
                Id = 0,
                Degree = "Bachelor",
                Email = line + "@gmail.com",
                DOB = new DateTime(2000, 1, 1),
                University = "ITU",
                AppliedProjects = new List<int>()
            });
            System.Console.WriteLine(res.Item1 == Status.Created ? "Created student, id: " + res.Item2 : "fail");
        }
    }