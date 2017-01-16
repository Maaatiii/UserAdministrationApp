using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Events;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.UserGroups
{
    public class UserGroupsAssigmentViewModel : BindableBase
    {
        public string HeaderInfo
        {
            get { return "Users in groups"; }
        }

        private readonly GroupRepository groupRepository;
        private readonly UserRepository userRepository;
        private readonly IEventAggregator eventAggregator;
        private ObservableCollection<UserModel> allUsers;
        private ObservableCollection<GroupModel> groups;

        public ObservableCollection<GroupModel> Groups
        {
            get { return groups; }
            set
            {
                groups = value; 
                base.OnPropertyChanged(nameof(Groups));
            }
        }

        public ObservableCollection<UserModel> AllUsers
        {
            get { return allUsers; }
            set
            {
                allUsers = value;
                base.OnPropertyChanged(nameof(AllUsers));
            }
        }

        public UserModel SelectedUser { get; set; }

        public DelegateCommand AddUserToGroupCommand { get; set; }
        public DelegateCommand<UserModel> DeleteUserFromGroupCommand { get; set; }

        public UserGroupsAssigmentViewModel(GroupRepository groupRepository, UserRepository userRepository, IEventAggregator eventAggregator)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.eventAggregator = eventAggregator;

            Load();

            eventAggregator.GetEvent<GroupCreatedEvent>().Subscribe(s => Load());
            eventAggregator.GetEvent<UserCreatedEvent>().Subscribe(s => Load());

            AddUserToGroupCommand = new DelegateCommand(AddUserToGroup);
            DeleteUserFromGroupCommand = new DelegateCommand<UserModel>(DeleteFromGroup);
        }

        private void DeleteFromGroup(UserModel user)
        {
            var group = Groups.SingleOrDefault(g => g.Users.Contains(user));
            if (group != null)
            {
                groupRepository.RemoveUserFromGroup(user.Id, group.Id);
                Load();
            }
        }

        private void AddUserToGroup()
        {
            if (SelectedGroup != null && SelectedUser != null)
            {
                var group = groupRepository.Get(SelectedGroup.Id, "Users");
                var user = userRepository.Get(SelectedUser.Id);

                groupRepository.AddUserToGroup(user.Id, group.Id);

                Load();
            }
        }

        private GroupModel SelectedGroup
        {
            get { return Groups.FirstOrDefault(g => g.IsSelected); }
        }

        private void Load()
        {
            AllUsers =
                new ObservableCollection<UserModel>(
                    userRepository.GetAll().Select(u => new UserModel(u)).ToList());

            Groups = new ObservableCollection<GroupModel>();
            var groups = this.groupRepository.GetAll("Users");
            foreach (var @group in groups)
            {
                var groupModel = new GroupModel(@group);
                //groupModel.Users =  @group.Users.Select(u => new UserModel() {Name = u.Name});
                Groups.Add(groupModel);
            }
        }
    }
}