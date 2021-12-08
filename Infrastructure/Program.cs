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
            var input = line.Split(" ");
            var res = await repo.UpdatePreferences(4, new PreferencesDTO{
                // Keywords = input[0].Split("-").ToList(),
                // Locations = input[1].Split("-").ToList(),
                // WorkDays = input[2].Split("-").ToList(),
                // Language = input[3]
                Keywords = new List<string>(){"AI", "Programming"},
                Locations = new List<LocationEnum>(){LocationEnum.Onsite},
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday, WorkdayEnum.Friday},
                Language = LanguageEnum.English
            });
            System.Console.WriteLine(res == Status.Updated ? "Succes" : "Fail");
        }
    }