using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Events;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Desktop.Shared.ViewModels;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupsForUserViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        
        public ObservableCollection<GroupModel> UserGroups { get; set; }

        public GroupsForUserViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<UserChangedEvent>().Subscribe(OnUserChanged);
        }

        private void OnUserChanged(UserChangedParams userChangedParams)
        {
            var userId = userChangedParams.UserId;
            if (userId.HasValue)
            {
                Load(userId.Value);
            }
        }

        private void Load(long userId)
        {
            var userRepo = new UserRepository();
            var user = userRepo.GetUserWithGroups(userId);
            UserGroups = new ObservableCollection<GroupModel>(user.Groups.Select(x => new GroupModel(x)));
        }
    }
}