using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Infrastructure;

public class LiteProjectBankContext : DbContext, IProjectBankContext
{
    public DbSet<Project> projects { get; set; } = null!;
    public DbSet<Student> students { get; set; } = null!;

    public string DbPath { get; private set; }

    public LiteProjectBankContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}liteProjectBank.db";
        Database.EnsureDeleted();
        if (Database.EnsureCreated()) {
            projects.Add(new Project
                {
                    Name = "Kunstig intelligens og indflydelse på UX", 
                    Id = 1,
                    Description = "I denne afhandling skal den studerende undersøge hvorvidt og hvordan kunstig intelligens kan anvendes på UX området.",
                    DueDate = new DateTime(2022, 4, 22),
                    IntendedWorkHours = 15,
                    SkillRequirementDescription = "Den studerende skal have bestået et tidligere kursus om kunstig intelligens.",
                    isThesis = true
                }
            );
            projects.Add(new Project
                {
                    Name = "Preference Matching", 
                    Id = 2,
                    Description = "For this thesis, the student has to write their own matching algorithm and reason for its quality.",
                    DueDate = new DateTime(2024, 6, 30),
                    IntendedWorkHours = 15,
                    SkillRequirementDescription = "The student should have prior experience in both java and algorithms",
                    isThesis = true
                }
            );
            SaveChanges();
        }
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}