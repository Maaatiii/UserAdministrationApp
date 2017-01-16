using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public interface IGroupRepository : IRepository<Group>
    {
        void AddUserToGroup(long userId, long groupId);
        void RemoveUserFromGroup(long userId, long groupId);
    }
}