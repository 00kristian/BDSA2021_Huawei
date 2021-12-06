// See https://aka.ms/new-console-template for more information
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        
    using (var db = new ProjectBankContextFactory().CreateDbContext(new string[0]))
    {
        
        
    }