using System;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public class UserEditorViewModel : CrudEditorViewModel<UserModel, User, IUserRepository>, INavigationAware
    {
        private readonly IEventAggregator eventAggregator;

        public UserEditorViewModel(IUserRepository repository, IEventAggregator eventAggregator) : base(repository)
        {            
            this.eventAggregator = eventAggregator;
        }

        protected override void Create(User entity)
        {
            entity.CreatedOn = DateTime.Now;
            repository.Create(entity);
            eventAggregator.GetEvent<UserCreatedEvent>().Publish(new UserCreatedParam());
        }

        protected override void Update(User entity)
        {
            repository.Update(entity);
        }

        protected override User CreateEmpty()
        {
            var user = new User()
            {
                CreatedOn = Item.CreatedOn,
                Email = Item.Email,
                Name = Item.Name,
                Id = Item.Id
            };

            return user;
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