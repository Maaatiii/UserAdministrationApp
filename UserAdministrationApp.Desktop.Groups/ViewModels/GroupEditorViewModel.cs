using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Desktop.Users.ViewModels;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupEditorViewModel : CrudEditorViewModel<GroupModel,Group,IGroupRepository>, INavigationAware
    {
        private readonly IEventAggregator eventAggregator;
        
        public GroupEditorViewModel(IGroupRepository repository, IEventAggregator eventAggregator) : base(repository)
        {
            this.eventAggregator = eventAggregator;
        }
        
        protected override void Create(Group entity)
        {
            repository.Create(entity);
            eventAggregator.GetEvent<GroupUpdatedEvent>().Publish(new GroupUpdatedParam());
        }

        protected override void Update(Group entity)
        {
            repository.Update(entity);
            eventAggregator.GetEvent<GroupUpdatedEvent>().Publish(new GroupUpdatedParam());
        }

        protected override Group CreateEmpty()
        {
            var group = new Group()
            {
                Name = Item.Name,
                Id = Item.Id
            };
            return group;
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