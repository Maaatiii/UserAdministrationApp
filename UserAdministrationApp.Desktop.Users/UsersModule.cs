using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared;
using UserAdministrationApp.Desktop.Users.Views;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Users
{
    public class UsersModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public UsersModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IUserRepository, UserRepository>();

            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UserAdministrationView));
            this.regionManager.RegisterViewWithRegion(RegionNames.UserEditor, typeof(UserEditorView));

            regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(UserAdministrationView).FullName, UriKind.Relative));
        }
    }
}
