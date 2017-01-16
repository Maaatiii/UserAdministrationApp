using UserAdministrationApp.Model;

namespace UserAdministrationApp.Services
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserWithGroups(long id);
    }
}