// See https://aka.ms/new-console-template for more information
using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        
using (var db = new ProjectBankContextFactory().CreateDbContext(new string[0]))
{
    db.Database.EnsureCreated();
    var repo = new StudentRepository(db);
    
    var res = await repo.Update(1, new StudentDTO(DegreeEnum.Bachelor, 1, "Lukas", 1, "Lukas@microsoft.com",
    new DateTime(2000, 06, 15), UniversityEnum.ITU, new List<int>()));
    System.Console.WriteLine(res == Status.Updated ? "Succes" : "Fail");
    
}