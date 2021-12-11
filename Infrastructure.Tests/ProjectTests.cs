using Xunit;
using System.Collections.Generic;
using Core;

namespace Infrastructure.Tests
{
    public class ProjectTests 
    {
        public Project project1;
        public Project project2;

        public Preferences preferences1;
        public Preferences preferences2;
        public Preferences preferences3;

        public ProjectTests ()
        {

            project1 = new Project{
                Name = "AI-Project",
                Id = 1,
                Description = "If you like artificial intelligence this project is for you",
                DueDate = new System.DateTime(2021,12,30),
                IntendedWorkHours = 50,
                Language = LanguageEnum.English,
                Keywords = new HashSet<Keyword>{new Keyword(){Str = "Machine Learning"}, new Keyword(){Str = "Python"}},
                SkillRequirementDescription = "Intro to AI",
                SupervisorName = "Kåre",
                Location = LocationEnum.Onsite,
                IsThesis = false,
                Meetingday = WorkdayEnum.Tuesday
            };

            project2 = new Project{
                Name = "Algorithms-Thesis",
                Id = 2,
                Description = "If you like Algorithms this thesis is for you",
                DueDate = new System.DateTime(2021,12,25),
                IntendedWorkHours = 100,
                Language = LanguageEnum.English,
                Keywords = new HashSet<Keyword>{new Keyword(){Str = "Programming"}, new Keyword(){Str = "Machine Learning"}},
                SkillRequirementDescription = "Intro to algorithms",
                SupervisorName = "Marie Dahl Esteban-Pedersen Sigurdsson",
                Location = LocationEnum.Onsite,
                IsThesis = true,
                Meetingday = WorkdayEnum.Friday
            };

            preferences1 = new Preferences{
                Keywords = new List<Keyword>(){new Keyword(){Str = "Programming"}, new Keyword(){Str = "Machine Learning"}},
                Location = LocationEnum.Onsite,
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Monday, WorkdayEnum.Tuesday, WorkdayEnum.Friday},
                Language = LanguageEnum.English
            };

            preferences2 = new Preferences{
                Keywords = new List<Keyword>(){new Keyword(){Str = "Database Management Systems"}, new Keyword(){Str = "SQL"}},
                Location = LocationEnum.Onsite,
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Friday, WorkdayEnum.Saturday},
                Language = LanguageEnum.English
            };

            preferences3 = new Preferences {
                Keywords = new List<Keyword>(){new Keyword(){Str = "Living life"}},
                Location = LocationEnum.Remote, 
                Workdays = new List<WorkdayEnum>(){WorkdayEnum.Friday, WorkdayEnum.Saturday},
                Language = LanguageEnum.Danish
            };
        }

        [Fact]
        public void match_returns_87_percent ()
        {
            var expected = 87;

            var actual = project1.Match(preferences1);

            Assert.Equal(expected, actual);       
        }

        [Fact]
        public void match_returns_0_percent ()
        {
            var expected = 0;

            var actual = project1.Match(preferences3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void match_returns_25_percent ()
        {
            var expected = 25;

            var actual = project2.Match(preferences3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void match_returns_75_percent ()
        {
            var expected = 75;

            var actual = project2.Match(preferences2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void match_returns_50_percent ()
        {
            var expected = 50;

            var actual = project1.Match(preferences2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void match_returns_something_percent()
        {
            var expected = 99;

            var actual = project2.Match(preferences1);

            Assert.Equal(expected, actual);
        }

    }


}