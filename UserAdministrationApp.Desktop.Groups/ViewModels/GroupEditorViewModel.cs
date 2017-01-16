using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupEditorViewModel : BindableBase, INavigationAware
    {
        private readonly GroupRepository repository;
        private readonly IEventAggregator eventAggregator;
        private GroupModel item;

        public GroupEditorViewModel(GroupRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
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
            var user = new Group()
            {
                Name = Item.Name,
                Id = Item.Id
            };

            if (Item.IsNew)
            {
                repository.Create(user);
                eventAggregator.GetEvent<GroupCreatedEvent>().Publish(new GroupCreatedParam());

            }
            else
            {
                repository.Update(user);
            }
        }

        private void OnSelectedUserOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand SaveCommand { get; set; }

        public GroupModel Item
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
            var itemParam = (GroupModel) navigationContext.Parameters["item"];
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