using System.Windows.Controls;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(LoginViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
