using System;
using Microsoft.Practices.Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using UserAdministrationApp.Desktop.Shared;
using UserAdministrationApp.Desktop.Users;

namespace UserAdministrationApp.Desktop
{
    public class ShellVM
    {
        private readonly IModuleManager moduleManager;
        public DelegateCommand LoadUserGroupModuleCommand { get; set; }

        public ShellVM(IModuleManager moduleManager)
        {
            this.moduleManager = moduleManager;
            LoadUserGroupModuleCommand = new DelegateCommand(LoadUserGroupModule);
        }

        private void LoadUserGroupModule()
        {
            moduleManager.LoadModule("UserGroupsModule");
        }
    }
}