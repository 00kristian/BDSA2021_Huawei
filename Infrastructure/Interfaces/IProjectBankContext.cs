using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public interface IProjectBankContext : IDisposable
{
    public DbSet<Project> projects { get; set; }
    public DbSet<Student> students {get;set;}
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}