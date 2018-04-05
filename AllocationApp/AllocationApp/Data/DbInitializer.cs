using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllocationApp.Models;
namespace AllocationApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(AllocationContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }
            var users = new User[]
            {
            new User{FirstName="Carson",LastName="Alexander"},
            new User{FirstName="Meredith",LastName="Alonso"},
            new User{FirstName="Arturo",LastName="Anand"},
            new User{FirstName="Gytis",LastName="Barzdukas"},
            new User{FirstName="Peggy",LastName="Justice"},
            new User{FirstName="Laura",LastName="Norman"},
            new User{FirstName="Nino",LastName="Olivetto"},

            /*new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}*/
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
            var Modules = new Module[]
            {
            new Module{ModuleID=1050,Name="Chemistry"},
            new Module{ModuleID=4022,Name="Microeconomics"},
            new Module{ModuleID=4041,Name="Macroeconomics"},
            new Module{ModuleID=1045,Name="Calculus"},
            new Module{ModuleID=3141,Name="Trigonometry"},
            new Module{ModuleID=2021,Name="Composition"},
            new Module{ModuleID=2042,Name="Literature"}
            };
            foreach (Module c in Modules)
            {
                context.Modules.Add(c);
            }
            context.SaveChanges();
            var Moduleusers = new ModuleUser[]
            {

                new ModuleUser{UserID=1,ModuleID=4022},
                new ModuleUser{UserID=1,ModuleID=4041},
                new ModuleUser{UserID=2,ModuleID=1045},
                new ModuleUser{UserID=2,ModuleID=3141},
                new ModuleUser{UserID=2,ModuleID=2021},
                new ModuleUser{UserID=3,ModuleID=1050},
                new ModuleUser{UserID=4,ModuleID=1050},
                new ModuleUser{UserID=4,ModuleID=4022},
                new ModuleUser{UserID=5,ModuleID=4041},
                new ModuleUser{UserID=6,ModuleID=1045},
                new ModuleUser{UserID=7,ModuleID=3141},
            };
            ModuleUser ModuleUser = new ModuleUser();
            ModuleUser.UserID = 1;
            ModuleUser.ModuleID = 1050;
            context.ModuleUsers.Add(ModuleUser);

            foreach (ModuleUser c in Moduleusers)
            {
                context.ModuleUsers.Add(c);
            }

            context.SaveChanges();
            var skills = new Skill[]
            {
                new Skill(1, "Java"),
                new Skill(2, "C"),
                new Skill(3, "C++"),
                new Skill(4, "ARM Assembly"),
                new Skill(5, "Go"),
            };
            foreach (Skill c in skills)
            {
                context.Skills.Add(c);
            }
            context.SaveChanges();
        }
    }
}