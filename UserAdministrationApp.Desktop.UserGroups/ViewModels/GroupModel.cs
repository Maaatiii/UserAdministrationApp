using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Desktop.UserGroups
{
    public class GroupModel : BindableBase
    {
        private bool isSelected;

        public GroupModel(Group group)
        {
            Name = group.Name;
            Id = group.Id;
            var userModels = @group.Users.Select(u => new UserModel(u));
            Users = new ObservableCollection<UserModel>(userModels);
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                base.OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string Name { get; set; }

        public ObservableCollection<UserModel> Users { get; set; }
        public long Id { get; private set; }
    }
}