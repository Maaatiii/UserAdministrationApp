using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Prism.Events;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared.Events;
using UserAdministrationApp.Desktop.Users.ViewModels;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Tests
{
    [TestFixture]
    public class UserAdministrationViewModelTests
    {
        [Test]
        public void when_i_start_application_users_are_loaded()
        {
            var users = new[] { new User(), new User() };
            var viewModel = CreateTestViewModel(users);

            Assert.AreEqual(viewModel.Items.Count, users.Length);
        }

        private static UserAdministrationViewModel CreateTestViewModel(User[] users)
        {
            var repository = new Mock<IUserRepository>();
            var eventAggregator = new Mock<IEventAggregator>();
            var regionManager = new Mock<IRegionManager>();
            eventAggregator.Setup(x => x.GetEvent<UserCreatedEvent>()).Returns(new UserCreatedEvent());
            eventAggregator.Setup(x => x.GetEvent<UserChangedEvent>()).Returns(new UserChangedEvent());
            repository.Setup(x => x.GetAll(It.IsAny<string>())).Returns(users);

            return new UserAdministrationViewModel(repository.Object, eventAggregator.Object, regionManager.Object);
        }

        [Test]
        public void when_i_add_new_user_selected_item_is_set_to_new_element()
        {
            var users = new User[] { };
            var viewModel = CreateTestViewModel(users);
            viewModel.AddCommand.Execute();

            Assert.AreEqual(1, viewModel.Items.Count);
            Assert.IsNotNull(viewModel.SelectedItem);
            Assert.AreEqual(true, viewModel.SelectedItem.IsNew);
        }

        [Test]
        public void when_i_execute_delete_element_entity_should_be_deleted_from_db()
        {
            var users = new User[] { new User() { Name = "Adam", Id = 1}, new User() { Name = "John" , Id = 2} };
            var repository = new Mock<IUserRepository>();
            var eventAggregator = new Mock<IEventAggregator>();
            var regionManager = new Mock<IRegionManager>();
            eventAggregator.Setup(x => x.GetEvent<UserCreatedEvent>()).Returns(new UserCreatedEvent());
            eventAggregator.Setup(x => x.GetEvent<UserChangedEvent>()).Returns(new UserChangedEvent());
            repository.Setup(x => x.GetAll(It.IsAny<string>())).Returns(users);
            var viewModel = new UserAdministrationViewModel(repository.Object, eventAggregator.Object, regionManager.Object);

            viewModel.SelectedItem = viewModel.Items.First();
            viewModel.DeleteCommand.Execute();

            repository.Verify(x=>x.Delete(users.First().Id),Times.Once);            
        }
    }
}
