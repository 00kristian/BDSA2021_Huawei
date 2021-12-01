using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Infrastructure;

public class LiteProjectBankContext : DbContext, IProjectBankContext
{
    public DbSet<Project> projects { get; set; }
    public DbSet<Student> students { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string DbPath { get; private set; }

    public LiteProjectBankContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}projectBank.db";
        Database.EnsureCreated();
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}