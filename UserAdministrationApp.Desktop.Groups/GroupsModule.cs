
using Prism.Modularity;
using Prism.Regions;
using UserAdministrationApp.Desktop.Groups.Views;
using UserAdministrationApp.Desktop.Shared;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupsModule : IModule
    {
        private readonly IRegionManager regionManager;

        public GroupsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(GroupsAdministrationView));
            this.regionManager.RegisterViewWithRegion(RegionNames.UserPluginRegion, typeof(UserGroupsView));
            this.regionManager.RegisterViewWithRegion(RegionNames.GroupEditor, typeof(GroupEditor));
        }
    }
}