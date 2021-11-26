using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_PB{

public class ProjectBankContext : DbContext
{
    public DbSet<Project> projects { get; set; }
    public DbSet<Student> students {get;set;}
    public DbSet<Keyword> keywords {get;set;}

    public string DbPath { get; private set; }

    public ProjectBankContext(DbContextOptions<ProjectBankContext> options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}projectBank.db";
        Database.EnsureCreated();
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Project>()
            .Property(p => p.Language)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<Language>());

        modelBuilder
            .Entity<Project>()
            .Property(p => p.Keywords)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<KeywordEnum>());

        modelBuilder
            .Entity<Project>()
            .Property(p => p.WorkDays)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<WorkDay>());

        modelBuilder
            .Entity<Project>()
            .Property(p => p.Locations)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<Location>());
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
}