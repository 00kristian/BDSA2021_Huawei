// See https://aka.ms/new-console-template for more information
using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        
    using (var db = new ProjectBankContextFactory().CreateDbContext(new string[0]))
    {
        db.Database.EnsureCreated();
        var repo = new StudentRepository(db);

            var res = await repo.Create( new StudentDTO{
                Name = "Lady Gaga",
                Id = 1,
                Degree = DegreeEnum.Master,
                Email = "AlejanThough@gmail.com",
                DOB = new DateTime(2009, 4, 4),
                University = UniversityEnum.RUC,
                AppliedProjects = new List<int>(),
                PreferenceId = 0
            });
            // var res = await repo.UpdatePreferences(4, new PreferencesDTO{
            //     // Keywords = input[0].Split("-").ToList(),
            //     // Locations = input[1].Split("-").ToList(),
            //     // WorkDays = input[2].Split("-").ToList(),
            //     // Language = input[3]
            //     Keywords = new List<string>(){"AI", "Programming"},
            //     Locations = new List<LocationEnum>(){LocationEnum.Onsite},
            //     Workdays = new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday, WorkdayEnum.Friday},
            //     Language = LanguageEnum.English
            // });
            System.Console.WriteLine(res.Item1 == Status.Created ? "Succes" : "Fail");
    }