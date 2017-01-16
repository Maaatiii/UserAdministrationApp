using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public class UserEditorViewModel : BindableBase, INavigationAware
    {
        private readonly UserRepository userRepository;
        private readonly IEventAggregator eventAggregator;
        private UserModel item;

        public UserEditorViewModel(UserRepository userRepository, IEventAggregator eventAggregator)
        {
            this.userRepository = userRepository;
            this.eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        private bool CanSave()
        {
            if (Item != null)
            {
                return string.IsNullOrWhiteSpace(Item.Error);
            }

            return false;
        }

        private void Save()
        {
            var user = new User()
            {
                CreatedOn = Item.CreatedOn,
                Email = Item.Email,
                Name = Item.Name,
                Id = Item.Id
            };

            if (Item.IsNew)
            {
                user.CreatedOn = DateTime.Now;
                userRepository.Create(user);
                eventAggregator.GetEvent<UserCreatedEvent>().Publish(new UserCreatedParam());
            }
            else
            {
                userRepository.Update(user);
            }            
        }

        private void OnSelectedUserOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand SaveCommand { get; set; }

        public UserModel Item
        {
            get { return item; }
            set
            {
                if (Item != null)
                {
                    Item.PropertyChanged -= OnSelectedUserOnPropertyChanged;
                }
                item = value;
                base.OnPropertyChanged(nameof(Item));

                SaveCommand.RaiseCanExecuteChanged();

                if (Item != null)
                {
                    Item.PropertyChanged += OnSelectedUserOnPropertyChanged;
                }
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var itemParam = (UserModel)navigationContext.Parameters["item"];
            Item = itemParam;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}