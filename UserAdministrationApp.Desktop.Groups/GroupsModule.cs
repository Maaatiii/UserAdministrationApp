
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using UserAdministrationApp.Desktop.Groups.Views;
using UserAdministrationApp.Desktop.Shared;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupsModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        public GroupsModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        public void Initialize()
        {
            unityContainer.RegisterType<IGroupRepository, GroupRepository>();

            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(GroupsAdministrationView));
            this.regionManager.RegisterViewWithRegion(RegionNames.UserPluginRegion, typeof(GroupsForUserView));
            this.regionManager.RegisterViewWithRegion(RegionNames.GroupEditor, typeof(GroupEditor));
        }
    }
}