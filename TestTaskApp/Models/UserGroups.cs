using System;
using System.Collections.Generic;

namespace TestTaskApp.Models
{
    public class UserGroups
    {
        public Guid Id { get; set; }

        public GroupsNameEnum GroupName { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
