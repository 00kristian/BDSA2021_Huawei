using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure {

public class ProjectBankContext : DbContext, IProjectBankContext
{
    public DbSet<Project> projects { get; set; } = null!;
    public DbSet<Student> students {get;set;} = null!;
    //public DbSet<Keyword> keywords {get;set;}

    public string DbPath { get; private set; }

    public ProjectBankContext(DbContextOptions<ProjectBankContext> options): base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}projectBank.db";
        Database.EnsureCreated();
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder
            .Entity<Project>()
            .Property(p => p.Language)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<Language>());*/

        /*modelBuilder
            .Entity<Project>()
            .Property(p => p.Keywords)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<KeywordEnum>());*/

        /*modelBuilder.Entity<Project>()
            .HasMany<ProjectKeyword>(project => project.Keywords)
            .WithOne(keyword => keyword.Project);*/

        /*modelBuilder.Entity<ProjectKeyword>()
            .HasOne<Keyword>(pk =>pk.Keyword)
            .WithMany(keyword => keyword.Projects);*/
        
        /*modelBuilder.Entity<Project>()
            .HasMany<KeywordEnum>()*/

        /*modelBuilder
            .Entity<Project>()
            .Property(p => p.WorkDays)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<WorkDay>());

        modelBuilder
            .Entity<Project>()
            .Property(p => p.Locations)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<Location>());*/
    }

    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Data Source={DbPath}");
}
}