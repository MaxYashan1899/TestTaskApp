using System.Linq;
using TestTaskApp.Models;

namespace TestTaskApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(UserContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Users.Any())
            {
                return;
            }

            var groupNameAdmin = new UserGroups { GroupName = GroupsNameEnum.Administrators };
            var groupNameUser = new UserGroups { GroupName = GroupsNameEnum.Users };

            dbContext.UsersGroups.AddRange(groupNameAdmin, groupNameUser);

            var userAdmin = new User {Login = "Admin", Password = BCrypt.Net.BCrypt.HashPassword("Admin") };
            var userMax = new User { Login = "Max", Password = BCrypt.Net.BCrypt.HashPassword("Max") };
            var userAlex = new User {Login = "Alex", Password = BCrypt.Net.BCrypt.HashPassword("Alex")};
            dbContext.Users.AddRange(userAdmin, userAlex, userMax);

            userAdmin.UsersGroups.Add(groupNameAdmin);
            userMax.UsersGroups.Add(groupNameAdmin);
            userAlex.UsersGroups.Add(groupNameAdmin);

            dbContext.SaveChanges();
        }
    }
}
