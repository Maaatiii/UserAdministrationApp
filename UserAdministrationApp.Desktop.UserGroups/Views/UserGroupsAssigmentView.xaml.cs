using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserAdministrationApp.Desktop.Shared.Views;

namespace UserAdministrationApp.Desktop.UserGroups
{
    /// <summary>
    /// Interaction logic for UserGroupsAssigmentView.xaml
    /// </summary>
    public partial class UserGroupsAssigmentView : MVVMViewBase
    {
        public UserGroupsAssigmentViewModel ViewModel
        {
            get { return (UserGroupsAssigmentViewModel) DataContext; }
        }

        public UserGroupsAssigmentView(UserGroupsAssigmentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void TrvMenu_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var groupModel = e.NewValue as GroupModel;
            if (groupModel != null)
            {
                ViewModel.SelectedGroup = groupModel;
            }
            else
            {
                ViewModel.SelectedGroup = null;
            }
        }
    }
}
