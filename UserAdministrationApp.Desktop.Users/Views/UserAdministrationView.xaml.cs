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
using UserAdministrationApp.Desktop.Users.ViewModels;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Users
{
    /// <summary>
    /// Interaction logic for UserAdministrationView.xaml
    /// </summary>
    public partial class UserAdministrationView : UserControl
    {
        public UserAdministrationView(UserAdministrationViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;           
        }
    }
}
