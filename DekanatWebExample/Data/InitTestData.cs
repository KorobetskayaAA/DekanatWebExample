using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DekanatWebExample.Models;

namespace DekanatWebExample.Data
{
    public class InitTestData : DropCreateDatabaseIfModelChanges<DekanatContext>
    {
        protected override void Seed(DekanatContext context)
        {
            var groups = new List<Group>
            {
                new Group() {
                    EducationForm = EducationForms.Ochnoe,
                    EducationProgram = "Applied Informatics in E-Economy",
                    Year = 1,
                    Number = 1
                },
                new Group() {
                    EducationForm = EducationForms.Ochnoe,
                    EducationProgram = "Applied Informatics in E-Economy",
                    Year = 2,
                    Number = 1
                },
                new Group() {
                    EducationForm = EducationForms.Ochnoe,
                    EducationProgram = "Applied Informatics in E-Economy",
                    Year = 3,
                    Number = 1
                },
                new Group() {
                    EducationForm = EducationForms.Zaochnoe,
                    EducationProgram = "Applied Informatics in E-Economy",
                    Year = 3,
                    Number = 1
                },
                new Group() {
                    EducationForm = EducationForms.Ochnoe,
                    EducationProgram = "State and Municipal Administration",
                    Year = 1,
                    Number = 1
                },
                new Group() {
                    EducationForm = EducationForms.Ochnoe,
                    EducationProgram = "State and Municipal Administration",
                    Year = 1,
                    Number = 2
                }
            };
            context.Groups.AddRange(groups);

            var students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Petr",
                    LastName = "Kutnetsov",
                    BirthDate = DateTime.Parse("2002-4-2"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Inna",
                    LastName = "Ermolayeva",
                    BirthDate = DateTime.Parse("2002-7-23"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Semen",
                    LastName = "Petrenko",
                    BirthDate = DateTime.Parse("2001-12-31"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Anna",
                    LastName = "Lutikova",
                    BirthDate = DateTime.Parse("2001-1-1"),
                    Group = groups[1]
                },
                new Student()
                {
                    FirstName = "Iraklij",
                    LastName = "Tupaev",
                    BirthDate = DateTime.Parse("2000-8-4"),
                    Group = groups[1]
                }
            };

            // studbilet numbers are, for instance, year followed by ordering number
            for (int i = 0; i < students.Count; i++)
            {
                students[i].Studbilet = (i + 200 + 
                    + (DateTime.Now.Year - 2000 + 1 - students[i].Group.Year) * 1000000).ToString();

            }

            context.Students.AddRange(students);
            context.SaveChanges();
        }
    }
}