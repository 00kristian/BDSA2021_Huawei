using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure{

    public class ProjectBankContextFactory : IDesignTimeDbContextFactory<ProjectBankContext>
    {
        public ProjectBankContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ProjectBankDB");
            var optionBuilder = new DbContextOptionsBuilder<ProjectBankContext>()
                .UseSqlServer(connectionString);

            return new ProjectBankContext(optionBuilder.Options);
        }
    }
}