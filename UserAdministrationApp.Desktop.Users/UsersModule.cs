using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared;
using UserAdministrationApp.Desktop.Users.Views;

namespace UserAdministrationApp.Desktop.Users
{
    public class UsersModule : IModule
    {
        private readonly IRegionManager regionManager;

        public UsersModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UserAdministrationView));
            this.regionManager.RegisterViewWithRegion(RegionNames.UserEditor, typeof(UserEditorView));

            regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(UserAdministrationView).FullName, UriKind.Relative));
        }
    }
}
