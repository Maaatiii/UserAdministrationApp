using System.Linq;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public void AddUserToGroup(long userId, long groupId)
        {
            using (var db = new UserContext())
            {
                var group = db.Groups.Include("Users").Single(u => u.Id == groupId);
                var user = db.Users.Single(u => u.Id == userId);
                group.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromGroup(long userId, long groupId)
        {
            using (var db = new UserContext())
            {
                var group = db.Groups.Include("Users").Single(u => u.Id == groupId);
                var user = db.Users.Single(u => u.Id == userId);
                group.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}