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

namespace UserAdministrationApp.Desktop.Users.Views
{
    /// <summary>
    /// Interaction logic for UserEditorView.xaml
    /// </summary>
    public partial class UserEditorView : UserControl
    {
        public UserEditorView(UserEditorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
