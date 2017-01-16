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

namespace UserAdministrationApp.Desktop.UserGroups
{
    /// <summary>
    /// Interaction logic for UserGroupsAssigmentView.xaml
    /// </summary>
    public partial class UserGroupsAssigmentView : UserControl
    {
        public UserGroupsAssigmentView(UserGroupsAssigmentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
