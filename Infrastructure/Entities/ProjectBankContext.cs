using System.Buffers;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure {

public class ProjectBankContext : DbContext, IProjectBankContext
{
    public DbSet<Project> projects { get; set; } = null!;
    public DbSet<Student> students {get;set;} = null!;
    public DbSet<Keyword> keywords {get;set;} = null!;
    public DbSet<Preferences> preferences {get;set;} = null!;
    public string DbPath { get; private set; } = null!;

    public ProjectBankContext(DbContextOptions<ProjectBankContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Keyword>()
        .HasIndex(s => s.Str)
        .IsUnique();

        modelBuilder.Entity<Project>()
        .HasMany<Keyword>(p => p.Keywords)
        .WithMany(keyword => keyword.Projects);

        modelBuilder.Entity<Preferences>()
        .HasMany<Keyword>(p => p.Keywords)
        .WithMany(k => k.Preferences);

        modelBuilder.Entity<Student>()
        .HasMany<Project>(s => s.AppliedProjects)
        .WithMany(p => p.Applications);

        modelBuilder.Entity<Student>()
        .HasOne(a => a.Preferences)
        .WithOne(b => b.Student)
        .HasForeignKey<Preferences>(b => b.StudentId);

        modelBuilder
        .Entity<Preferences>()
        .Property(p => p.Workdays)
        .HasConversion(
            v => JsonSerializer.Serialize(v.Select(d => d.ToString()).ToList(), default(JsonSerializerOptions)),
            v => JsonSerializer.Deserialize<List<string>>(v, default(JsonSerializerOptions))!.Select(d => (WorkdayEnum)Enum.Parse(typeof(WorkdayEnum), d)).ToList(),
            new ValueComparer<ICollection<WorkdayEnum>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList())
        );

        modelBuilder
        .Entity<Project>()
        .Property(p => p.Meetingday)
        .HasConversion(
            v => v.ToString(),
            v => (WorkdayEnum)Enum.Parse(typeof(WorkdayEnum), v));

        modelBuilder
        .Entity<Preferences>()
        .Property(p => p.Language)
        .HasConversion(
            v => v.ToString(),
            v => (LanguageEnum)Enum.Parse(typeof(LanguageEnum), v));

        modelBuilder
        .Entity<Preferences>()
        .Property(p => p.Location)
        .HasConversion(
            v => v.ToString(),
            v => (LocationEnum)Enum.Parse(typeof(LocationEnum), v));

        modelBuilder
        .Entity<Project>()
        .Property(p => p.Language)
        .HasConversion(
            v => v.ToString(),
            v => (LanguageEnum)Enum.Parse(typeof(LanguageEnum), v));

        modelBuilder
        .Entity<Project>()
        .Property(p => p.Location)
        .HasConversion(
            v => v.ToString(),
            v => (LocationEnum)Enum.Parse(typeof(LocationEnum), v));

        modelBuilder
        .Entity<Student>()
        .Property(s => s.Degree)
        .HasConversion(
            v => v.ToString(),
            v => (DegreeEnum)Enum.Parse(typeof(DegreeEnum), v));

        modelBuilder
        .Entity<Student>()
        .Property(s => s.University)
        .HasConversion(
            v => v.ToString(),
            v => (UniversityEnum)Enum.Parse(typeof(UniversityEnum), v));
        
    }
}}