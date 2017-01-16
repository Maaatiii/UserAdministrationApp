using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        //public DbSet<Group> UserGroups { get; set; }
    }
}
