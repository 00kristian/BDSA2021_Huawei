using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Server.Controllers;
using Xunit;

namespace Server.Tests{
    public class ProjectsKeywordControllerTests {

        static readonly ProjectDTO p1 = new ProjectDTO{
        Name = "AI-Project",
        Id = 1,
        Description = "If you like artificial intelligence this project is for you",
        DueDate = new System.DateTime(2021,12,30),
        IntendedWorkHours = 50,
        Language = LanguageEnum.English,
        Keywords = new List<string>{"Machine Learning", "Python"},
        SkillRequirementDescription = "Intro to machine learning",
        SupervisorName = "Kåre",
        Location = LocationEnum.Onsite,
        IsThesis = true 
    };
    
        [Fact]
        public async  void get_returns_all_Keywords()
        {
            
            var logger = new Mock<ILogger<KeywordsController>>();
        
            var repository = new Mock<IProjectRepository>();
        }




    }












}