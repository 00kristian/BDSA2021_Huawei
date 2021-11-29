using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Core{

    public class ProjectBankContextFactory : IDesignTimeDbContextFactory<ProjectBankContext>
    {
        public ProjectBankContext CreateDbContext(string[] args)
        {
            
            var connectionString = "User ID=postgres;Password=bdsa;Host=localhost;Port=1433;Database=BDSAExam;";
            var optionBuilder = new DbContextOptionsBuilder<ProjectBankContext>()
                .UseSqlServer(connectionString);

            return new ProjectBankContext(optionBuilder.Options);
        }

    }
}