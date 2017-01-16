using System;
using Prism.Modularity;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared;

namespace UserAdministrationApp.Desktop.UserGroups
{
    public class UserGroupsModule : IModule
    {
        private readonly IRegionManager regionManager;

        public UserGroupsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(UserGroupsAssigmentView));

            regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(UserGroupsAssigmentView).FullName, UriKind.Relative));
        }
    }
}