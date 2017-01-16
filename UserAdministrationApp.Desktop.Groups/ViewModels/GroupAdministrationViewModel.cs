using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Groups.Views;
using UserAdministrationApp.Desktop.Shared;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Desktop.Shared.ViewModels;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupAdministrationViewModel : CrudViewModel<GroupModel>
    {
        public string HeaderInfo => "Groups";

        private readonly IGroupRepository repository;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;

        public GroupAdministrationViewModel(IGroupRepository repository, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<GroupUpdatedEvent>().Subscribe(OnGroupUpdated);
        }

        private void OnGroupUpdated(GroupUpdatedParam obj)
        {
            SelectedItem = null;
            Load();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();

            if (SelectedItem != null)
            {
                var navParams = new NavigationParameters();
                navParams.Add("item", SelectedItem);
                regionManager.RequestNavigate(RegionNames.GroupEditor, new Uri(typeof(GroupEditor).FullName, UriKind.Relative), navParams);
            }            
        }

        protected sealed override void Load()
        {
            var groups = repository.GetAll();
            Items = new ObservableCollection<GroupModel>(groups.Select(g => new GroupModel(g)));
        }

        protected override void Add()
        {
            var item = new GroupModel();
            Items.Add(item);
            SelectedItem = item;
        }

        protected override void Delete()
        {
            repository.Delete(SelectedItem.Id);
            Load();
        }
    }
}