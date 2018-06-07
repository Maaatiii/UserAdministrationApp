
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using UserAdministrationApp.Desktop.Groups;
using UserAdministrationApp.Desktop.Users;

namespace UserAdministrationApp.Desktop
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();   
        }

        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void ConfigureModuleCatalog()
        {
			base.ConfigureModuleCatalog();

	        var pluginPath = @"c:\temp\s\UserAdministrationApp.Desktop.UserGroups.dll";

	        ModuleCatalog.AddModule(new ModuleInfo()
			{
				ModuleName = "UserGroupsModule",
				ModuleType = "UserAdministrationApp.Desktop.UserGroups.UserGroupsModule, UserAdministrationApp.Desktop.UserGroups, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
				Ref = new Uri(pluginPath, UriKind.RelativeOrAbsolute).AbsoluteUri,
				InitializationMode = InitializationMode.OnDemand
			});

			//Type moduleCType = typeof(UsersModule);
			//ModuleCatalog.AddModule(
			//  new ModuleInfo()
			//  {
			//      ModuleName = moduleCType.Name,
			//      ModuleType = moduleCType.AssemblyQualifiedName,
			//  });

			//Type groupsModule = typeof(GroupsModule);
			//ModuleCatalog.AddModule(
			//  new ModuleInfo()
			//  {
			//      ModuleName = groupsModule.Name,
			//      ModuleType = groupsModule.AssemblyQualifiedName,
			//  });

			//Type userGroupsModule = typeof(UserGroupsModule);
			//ModuleCatalog.AddModule(
			//  new ModuleInfo()
			//  {
			//      ModuleName = userGroupsModule.Name,
			//      ModuleType = userGroupsModule.AssemblyQualifiedName,
			//  });
		}

		protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
