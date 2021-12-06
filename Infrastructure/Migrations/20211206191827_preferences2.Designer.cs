﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ProjectBankContext))]
    [Migration("20211206191827_preferences2")]
    partial class preferences2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Preferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("Infrastructure.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IntendedWorkHours")
                        .HasColumnType("int");

                    b.Property<bool>("IsThesis")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillRequirementDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("StudentId");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("Infrastructure.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PreferenceId")
                        .HasColumnType("int");

                    b.Property<int>("University")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("students");
                });

            modelBuilder.Entity("Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Str")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("KeywordPreferences", b =>
                {
                    b.Property<int>("KeywordsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("KeywordsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("KeywordPreferences");
                });

            modelBuilder.Entity("KeywordProject", b =>
                {
                    b.Property<int>("KeywordsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("KeywordsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("KeywordProject");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PreferencesId")
                        .HasColumnType("int");

                    b.Property<string>("Str")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PreferencesId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Workday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PreferencesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PreferencesId");

                    b.ToTable("Workday");
                });

            modelBuilder.Entity("Infrastructure.Project", b =>
                {
                    b.HasOne("Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Infrastructure.Student", null)
                        .WithMany("AppliedProjects")
                        .HasForeignKey("StudentId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("KeywordPreferences", b =>
                {
                    b.HasOne("Keyword", null)
                        .WithMany()
                        .HasForeignKey("KeywordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Preferences", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KeywordProject", b =>
                {
                    b.HasOne("Keyword", null)
                        .WithMany()
                        .HasForeignKey("KeywordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.HasOne("Infrastructure.Preferences", null)
                        .WithMany("Locations")
                        .HasForeignKey("PreferencesId");
                });

            modelBuilder.Entity("Workday", b =>
                {
                    b.HasOne("Infrastructure.Preferences", null)
                        .WithMany("Workdays")
                        .HasForeignKey("PreferencesId");
                });

            modelBuilder.Entity("Infrastructure.Preferences", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Workdays");
                });

            modelBuilder.Entity("Infrastructure.Student", b =>
                {
                    b.Navigation("AppliedProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
