using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Build.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Desktop.Shared.ViewModels;
using UserAdministrationApp.Desktop.Users.Views;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public sealed class UserAdministrationViewModel : CrudViewModel<UserModel>
    {
        public string HeaderInfo
        {
            get { return "Users"; }
        }

        private readonly UserRepository repository;

        private readonly IEventAggregator eventAggregator;

        private readonly IRegionManager regionManager;

        public UserAdministrationViewModel(UserRepository repository, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.repository = repository;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;

            Load();

            eventAggregator.GetEvent<UserCreatedEvent>().Subscribe(OnUserCreated);
        }

        private void OnUserCreated(UserCreatedParam obj)
        {
            Load();
        }

        protected override void SelectedItemChanged()
        {
            base.SelectedItemChanged();

            if (SelectedItem != null)
            {
                var navParams = new NavigationParameters();
                navParams.Add("item", SelectedItem);
                regionManager.RequestNavigate(RegionNames.UserEditor,
                    new Uri(typeof(UserEditorView).FullName, UriKind.Relative), navParams);
            }

            if (SelectedItem != null && !SelectedItem.IsNew)
            {
                eventAggregator.GetEvent<UserChangedEvent>().Publish(new UserChangedParams(SelectedItem.Id));
            }
        }

        protected override void Delete()
        {
            repository.Delete(SelectedItem.Id);
            Load();
        }

        protected override void Add()
        {
            var user = new UserModel();
            Items.Add(user);
            SelectedItem = user;
        }

        protected override void Load()
        {
            var users = repository.GetAll("Groups");
            Items = new ObservableCollection<UserModel>(users.ToList().Select(ConvertToVM));
        }

        private UserModel ConvertToVM(User user)
        {
            return new UserModel(user);
        }
    }
}