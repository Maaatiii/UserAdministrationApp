using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Events;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Desktop.Shared.ViewModels;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.UserGroups
{
    public class UserGroupsAssigmentViewModel : ViewModelBase
    {
        public string HeaderInfo => "Users in groups";

        private readonly IGroupRepository groupRepository;
        private readonly IUserRepository userRepository;
        private readonly IEventAggregator eventAggregator;
        private List<UserModel> users;
        private ObservableCollection<GroupModel> groups;
        private GroupModel selectedGroup;
        private List<UserModel> allUsers;

        public ObservableCollection<GroupModel> Groups { get; set; }
        
        public List<UserModel> Users { get; set; }

        public UserModel SelectedUser { get; set; }

        public DelegateCommand AddUserToGroupCommand { get; set; }

        public DelegateCommand<UserModel> DeleteUserFromGroupCommand { get; set; }

        public UserGroupsAssigmentViewModel(IGroupRepository groupRepository, IUserRepository userRepository, IEventAggregator eventAggregator)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.eventAggregator = eventAggregator;

            Load();

            eventAggregator.GetEvent<GroupUpdatedEvent>().Subscribe(s => Load());
            eventAggregator.GetEvent<UserUpdatedEvent>().Subscribe(s => Load());

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

        public GroupModel SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;

                RefreshAvailableUsers();
            }
        }

        private void RefreshAvailableUsers()
        {
            if (SelectedGroup != null)
            {
                Users =
                    new List<UserModel>(
                        allUsers.Where(u => selectedGroup.Users.All(x => x.Id != u.Id)));
            }
            else
            {
                Users = null;
            }
        }

        private void Load()
        {
            allUsers =                
                userRepository.GetAll().Select(u => new UserModel(u)).ToList();

            Groups = new ObservableCollection<GroupModel>();
            var groups = this.groupRepository.GetAll("Users");
            foreach (var @group in groups)
            {
                var groupModel = new GroupModel(@group);
                //groupModel.Users =  @group.Users.Select(u => new UserModel() {Name = u.Name});
                Groups.Add(groupModel);
            }
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
            Load();
        }
    }
}