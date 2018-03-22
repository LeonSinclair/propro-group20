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
            var courses = new Course[]
            {
            new Course{CourseID=1050,Name="Chemistry"},
            new Course{CourseID=4022,Name="Microeconomics"},
            new Course{CourseID=4041,Name="Macroeconomics"},
            new Course{CourseID=1045,Name="Calculus"},
            new Course{CourseID=3141,Name="Trigonometry"},
            new Course{CourseID=2021,Name="Composition"},
            new Course{CourseID=2042,Name="Literature"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();
            var courseusers = new CourseUser[]
            {
            new CourseUser{UserID=1,CourseID=1050},            
            new CourseUser{UserID=1,CourseID=4022},
            new CourseUser{UserID=1,CourseID=4041},
            new CourseUser{UserID=2,CourseID=1045},
            new CourseUser{UserID=2,CourseID=3141},
            new CourseUser{UserID=2,CourseID=2021},
            new CourseUser{UserID=3,CourseID=1050},
            new CourseUser{UserID=4,CourseID=1050},
            new CourseUser{UserID=4,CourseID=4022},
            new CourseUser{UserID=5,CourseID=4041},
            new CourseUser{UserID=6,CourseID=1045},
            new CourseUser{UserID=7,CourseID=3141},
            };
            foreach (CourseUser c in courseusers)
            {
                context.CourseUsers.Add(c);
            }
            context.SaveChanges();

        }
    }
}
