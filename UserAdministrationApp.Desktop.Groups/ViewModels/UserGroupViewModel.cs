using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Events;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Groups
{
    public class UserGroupViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<GroupModel> _userGroups;

        public ObservableCollection<GroupModel> UserGroups
        {
            get { return _userGroups; }
            set
            {
                _userGroups = value;
                base.OnPropertyChanged(() => UserGroups);
            }
        }

        public UserGroupViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(OnUserChanged);
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