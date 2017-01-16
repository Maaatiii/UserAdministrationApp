using System.Windows;
using System.Windows.Controls;
using UserAdministrationApp.Desktop.Shared.ViewModels;

namespace UserAdministrationApp.Desktop.Shared.Views
{
    public class MVVMViewBase : UserControl
    {
        private ViewModelBase ViewModel
        {
            get { return (ViewModelBase)DataContext; }
        }

        public MVVMViewBase()
        {
            Loaded += MVVMViewBase_Loaded;
            Unloaded += MVVMViewBase_Unloaded;
        }

        private void MVVMViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.OnUnloaded();
        }

        private void MVVMViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.OnLoaded();
        }
    }
}