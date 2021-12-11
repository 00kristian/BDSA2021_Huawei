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
    
         [Fact]
        public async void get_returns_all_Keywords()
        {
            
            var logger = new Mock<ILogger<ProjectsController>>();
            var repository = new Mock<IProjectRepository>();

            
             var expected = new List<string>();

            repository.Setup(m => m.ReadAllKeywords()).ReturnsAsync(expected);
            var controller = new KeywordsController(logger.Object, repository.Object);

            var actual = await controller.Get();
            
            Assert.Equal(expected, actual);


        } 
    }
}