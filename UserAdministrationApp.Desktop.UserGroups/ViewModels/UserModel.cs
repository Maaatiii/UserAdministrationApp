using UserAdministrationApp.Model;

namespace UserAdministrationApp.Desktop.UserGroups
{
    public class UserModel
    {
        public UserModel(User user)
        {
            Name = user.Name;
            Id = user.Id;
        }

        public string Name { get; set; }
        public long Id { get; private set; }
    }
}