using System.Data.SqlClient;
using EF_PB;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Model.Tests;

public class ProjectRepositoryTests
{
    private readonly ProjectBankContext _context;
    private readonly ProjectRepository _repo;
   
    public ProjectRepositoryTests(){
        
        var connection = new SqlConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        builder.UseSqlite(connection);
        var context = new ProjectBankContext(builder.Options);
        context.Database.EnsureCreated();
        
        var project1 = new Shared.Project{
            Name = "AI-Project",
            Id = 1,
            Description = "If you like artificial intelligence this project is for you",
            DueDate = new System.DateTime(2021,12,30),
            IntendedWorkHours = 50,
            Language = Language.English,
            Keywords = new List<Keyword>{Keyword.AI},
            SkillRequirementDescription = "Intro to AI",
            Supervisor = new Shared.Supervisor(),
            WorkDays = new List<WorkDay>{WorkDay.Monday, WorkDay.Tuesday, WorkDay.Friday},
            Locations = new List<Location>{Location.Onsite},
            isThesis = false
        };

        var project2 = new Shared.Project{
            Name = "Algorithms-Thesis",
            Id = 2,
            Description = "If you like Algorithms this thesis is for you",
            DueDate = new System.DateTime(2021,12,25),
            IntendedWorkHours = 100,
            Language = Language.English,
            Keywords = new List<Keyword>{Keyword.Algorithms, Keyword.Python},
            SkillRequirementDescription = "Intro to algorithms",
            Supervisor = new Shared.Supervisor(),
            WorkDays = new List<WorkDay>{WorkDay.Tuesday},
            Locations = new List<Location>{Location.Remote},
            isThesis = true
        };

        context.projects.AddRange(
            project1,
            project2
        );

        context.SaveChanges();
        _context = context;
        _repo = new ProjectRepository(_context);
    }

    [Fact]
    public void can_Create_New_Project(){
        //Arrange
        var expected = new Shared.Project{
           Name = "Machine Learning project",
            Id = 3,
            Description = "If you like Machine Learning this project is for you",
            DueDate = new System.DateTime(2021,12,25),
            IntendedWorkHours = 75,
            Language = Language.English,
            Keywords = new List<Keyword>{Keyword.Algorithms, Keyword.Python},
            SkillRequirementDescription = "Intro to machine learning",
            Supervisor = new Shared.Supervisor(),
            WorkDays = new List<WorkDay>{WorkDay.Tuesday, WorkDay.Wednesday},
            Locations = new List<Location>{Location.Remote},
            isThesis = true 
        };

        //Act
        var actual = _repo.Create(expected);

        //Assert
        Assert.Equal(expected.Id, actual);
    }
    [Fact]
    public void read_given_2_Get_Project2()
    {
        //Arrange
        var expected = new Shared.Project{
            Name = "Algorithms-Project",
            Id = 2,
            Description = "If you like Algorithms this project is for you",
            DueDate = new System.DateTime(2021,12,25),
            IntendedWorkHours = 100,
            Language = Language.English,
            Keywords = new List<Keyword>{Keyword.Algorithms, Keyword.Python},
            SkillRequirementDescription = "Intro to algorithms",
            Supervisor = new Shared.Supervisor(),
            WorkDays = new List<WorkDay>{WorkDay.Tuesday},
            Locations = new List<Location>{Location.Remote},
            isThesis = true
        };

        //Act
        var actual = _repo.Read(2);

        //Assert
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void can_Update_Name(){
        //Arrange
        var project = new Shared.Project{
            Name = "Algorithms-Thesis/Project",
            Id = 2,
            Description = "If you like Algorithms this thesis is for you",
            DueDate = new System.DateTime(2021,12,25),
            IntendedWorkHours = 100,
            Language = Language.English,
            Keywords = new List<Keyword>{Keyword.Algorithms, Keyword.Python},
            SkillRequirementDescription = "Intro to algorithms",
            Supervisor = new Shared.Supervisor(),
            WorkDays = new List<WorkDay>{WorkDay.Tuesday},
            Locations = new List<Location>{Location.Remote},
            isThesis = true
        };

        //Act
        _repo.Update(project);

        //Assert
        Assert.Equal("Algorithms-Thesis/Project", _repo.Read(2).Name);

    }

    [Fact]
    public void can_Delete_Project(){
        //Act
        _repo.Delete(1);

        //Assert 
         Assert.Throws<InvalidOperationException>(()=>{
                _repo.Read(1);
            });
    }
}