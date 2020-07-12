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
            var groups = new List<Group>();
            var random = new Random(12);

            foreach (var edProgram in new string[] { "Applied Informatics in E-Economy",
                "State and Municipal Administration", "Management in Organizations", "Ensurance and Taxes", 
                "Accounting and Audition", "Regional Tourism", "Services in Hotel and Restaurant Business"})
            {
                foreach (EducationForms edForm in Enum.GetValues(typeof(EducationForms)))
                {
                    for (int year = 1; year <= 4; year++)
                    {
                        for (int num = 1; num <= random.Next(5) + 1; num++)
                        {
                            groups.Add(new Group()
                            {
                                EducationForm = edForm,
                                EducationProgram = edProgram,
                                Year = year,
                                Number = num
                            });
                            if (edForm == EducationForms.Zaochnoe)
                                break;
                        }
                    }
                }
            }
            context.Groups.AddRange(groups);

            var students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Petr", LastName = "Kuznetsov", BirthDate = DateTime.Parse("2002-4-2"), 
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Inna", LastName = "Ermolayeva", BirthDate = DateTime.Parse("2002-7-23"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Semen", LastName = "Petrenko", BirthDate = DateTime.Parse("2001-12-31"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Ulyana", LastName = "Efimova", BirthDate = DateTime.Parse("2000-5-12"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Tatyana", LastName = "Kutuzova", BirthDate = DateTime.Parse("2001-1-8"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Timofej", LastName = "Ruzaev", BirthDate = DateTime.Parse("2001-7-19"),
                    Group = groups[0]
                },
                new Student()
                {
                    FirstName = "Anna", LastName = "Lutikova", BirthDate = DateTime.Parse("2001-1-1"),
                    Group = groups[1]
                },
                new Student()
                {
                    FirstName = "Iraklij", LastName = "Tupaev", BirthDate = DateTime.Parse("2000-8-4"),
                    Group = groups[1]
                },
                new Student()
                {
                    FirstName = "Timur", LastName = "Kulinov", BirthDate = DateTime.Parse("2000-8-4"),
                    Group = groups[2]
                },
                new Student()
                {
                    FirstName = "Tamara", LastName = "Skvortsova", BirthDate = DateTime.Parse("2000-8-4"),
                    Group = groups[3]
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