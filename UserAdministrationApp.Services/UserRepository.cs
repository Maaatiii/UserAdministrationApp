using System.Collections;
using System.Linq;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public User GetUserWithGroups(long id)
        {
            using (var db = new UserContext())
            {
                return db.Set<User>().Include("Groups").Single(u => u.Id == id);
            }
        }
    }
}