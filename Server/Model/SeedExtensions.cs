using System.Threading;
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Core;

namespace Server.Model;

public static class SeedExtensions
{
    public static async Task<IHost> SeedAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ProjectBankContext>();

            await SeedProjectsAsync(context);
        }
        return host;
    }

    private static async Task SeedProjectsAsync(ProjectBankContext context)
    {
        if (context.projects.Count() > 1) return;

        await context.Database.MigrateAsync();

        if (!await context.projects.AnyAsync())
        {
            var description0 = "For this project, the student will have to write their own encryption methods in order to withstand cyber attacks.";
            var description1 = "I denne afhandling skal den studerende undersøge hvorvidt og hvordan kunstig intelligens kan anvendes på UX området.";
            var description2 = "For this thesis, the student has to write their own matching algorithm and reason for its quality.";
            var description3 = "I dette projekt skal den studerende designe en ny hjemmeside i samarbejde med et lokalt firma.";
            var description4 = "In this project, the student will be tasked with improving a speech recognition software";
            var description5 = "This is a thesis topic where the student will dive into what happens when AI misbehaves and how we would fix it with current knowledge.";
            var description6 = "I dette projekt vil den studerene lave en ny sorteringsalgoritme til PostNord, som er værre end den de allerede bruger.";
            var description7 = "For this project the student will create a whole new website for the Pentagon.";
            var description8 = "For this project the student will create an AI that reads the labels of chips bags and writes a review of it.";
            var description9 = "For this project the student will write a paper and produce parts of an algorithm to do with Social Media like Facebook, Twitter or Instagram.";

            var skill0 = "The student is expected to have taken a course on security and knows how to code in python.";
            var skill1 = "Den studerende skal have bestået et tidligere kursus om kunstig intelligens.";
            var skill2 = "The student should have prior experience in both C# and algorithms.";
            var skill3 = "Den studerende skal have bestået tidligere kurser om introduktion til UI og UX.";
            var skill4 = "The students needs to have prior experience with both machine learning and AI.";
            var skill5 = "The student is expected to have knowledge of Machine Learning and AI.";
            var skill6 = "Den studerende forventes at have bestået kurser i grundlæggende programmering og algoritmer.";
            var skill7 = "The student is expected to have fundamental knowledge of website and UI design.";
            var skill8 = "The student is expected to have passed a course in Natural Language Processing and have a fundamental understanding of Python.";
            var skill9 = "The student is expected to know the programming language F# and have taken a course on algorithms.";

            var Java = new Keyword {Str = "Java"};
            var Python = new Keyword {Str = "Python"};
            var CSharp = new Keyword {Str = "CSharp"};
            var FSharp = new Keyword {Str = "FSharp"};
            var AI = new Keyword {Str = "AI"};
            var Algorithms = new Keyword {Str = "Algorithms"};
            var MachineLearning = new Keyword {Str = "MachineLearning"};
            var Security = new Keyword {Str = "Security"};
            var UI = new Keyword {Str = "UI"};
            var UX = new Keyword {Str = "UX"};

            var date0 = new System.DateTime(2022,05,15);
            var date1 = new System.DateTime(2022,04,30);
            var date2 = new System.DateTime(2022,03,30);
            var date3 = new System.DateTime(2022,03,01);
            var date4 = new System.DateTime(2022,05,30);
            var date5 = new System.DateTime(2022,10,07);
            var date6 = new System.DateTime(2022,05,05);
            var date7 = new System.DateTime(2022,02,14);
            var date8 = new System.DateTime(2022,04,01);
            var date9 = new System.DateTime(2022,12,16);

            var hour0 = 0; //Less than 15 hours
            var hour1 = 1; //Between 15 and 25 hours
            var hour2 = 2; // Over 25 hours

            context.projects.AddRange(
                new Project() {Name = "The fight against cyber attacks", Description = description0, DueDate = date0, IntendedWorkHours = hour1, Language = LanguageEnum.English, SkillRequirementDescription = skill0, SupervisorName = "Sonja Pedersen", Location = LocationEnum.Onsite, IsThesis = false, Keywords = new List<Keyword> {Security, Python}, Meetingday = WorkdayEnum.Tuesday},
                new Project() {Name = "Kunstig intelligens og indflydelse på UX", Description = description1, DueDate = date1, IntendedWorkHours = hour2, Language = LanguageEnum.Danish, SkillRequirementDescription = skill1, SupervisorName = "Lars Larsen", Location = LocationEnum.Remote, IsThesis = true, Keywords = new List<Keyword> {UX, AI}, Meetingday = WorkdayEnum.Wednesday},
                new Project() {Name = "Preference Matching", Description = description2, DueDate = date2, IntendedWorkHours = hour2, Language = LanguageEnum.English, SkillRequirementDescription = skill2, SupervisorName = "Kingsley Richards", Location = LocationEnum.Remote, IsThesis = true, Keywords = new List<Keyword> {CSharp, Algorithms}, Meetingday = WorkdayEnum.Monday},
                new Project() {Name = "Hjemmeside design og brugeroplevelse", Description = description3, DueDate = date3, IntendedWorkHours = hour0, Language = LanguageEnum.Danish, SkillRequirementDescription = skill3, SupervisorName = "Håkan Lundell", Location = LocationEnum.Onsite, IsThesis = false, Keywords = new List<Keyword> {UI, UX}, Meetingday = WorkdayEnum.Friday},
                new Project() {Name = "Improving speech recognition", Description = description4, DueDate = date4, IntendedWorkHours = hour1, Language = LanguageEnum.English, SkillRequirementDescription = skill4, SupervisorName = "Leigh Duke", Location = LocationEnum.Onsite, IsThesis = false, Keywords = new List<Keyword> {MachineLearning, AI}, Meetingday = WorkdayEnum.Thursday},
                new Project() {Name = "Misbehaving AI", Description = description5, DueDate = date5, IntendedWorkHours = hour2, Language = LanguageEnum.English, SkillRequirementDescription = skill5, SupervisorName = "Leon Waters", Location = LocationEnum.Onsite, IsThesis = true, Keywords = new List<Keyword> {AI, MachineLearning}, Meetingday = WorkdayEnum.Friday},
                new Project() {Name = "Lav en forværret sorteringsalgoritme til PostNord", Description = description6, DueDate = date6, IntendedWorkHours = hour0, Language = LanguageEnum.Danish, SkillRequirementDescription = skill6, SupervisorName = "Jens Jensen", Location = LocationEnum.Remote, IsThesis = false, Keywords = new List<Keyword> {Java, Algorithms}, Meetingday = WorkdayEnum.Monday},
                new Project() {Name = "Create a new website for the Pentagon", Description = description7, DueDate = date7, IntendedWorkHours = hour1, Language = LanguageEnum.English, SkillRequirementDescription = skill7, SupervisorName = "Mike Pence", Location = LocationEnum.Onsite, IsThesis = false, Keywords = new List<Keyword> {UI}, Meetingday = WorkdayEnum.Tuesday},
                new Project() {Name = "Creating an AI for reviewing chips", Description = description8, DueDate = date8, IntendedWorkHours = hour1, Language = LanguageEnum.English, SkillRequirementDescription = skill8, SupervisorName = "Richard McHarth", Location = LocationEnum.Remote, IsThesis = false, Keywords = new List<Keyword> {Python, MachineLearning, AI}, Meetingday = WorkdayEnum.Wednesday},
                new Project() {Name = "Social Media Algorithms", Description = description9, DueDate = date9, IntendedWorkHours = hour0, Language = LanguageEnum.English, SkillRequirementDescription = skill9, SupervisorName = "Kurt Kurtsen", Location = LocationEnum.Onsite, IsThesis = false, Keywords = new List<Keyword> {FSharp, Algorithms}, Meetingday = WorkdayEnum.Thursday}
            );

            await context.SaveChangesAsync();
        }
    }
}