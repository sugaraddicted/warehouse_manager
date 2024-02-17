using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.ViewModel;
using Warehouse_Manager.State.Authenticators;


namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for ShoppingCartPage.xaml
    /// </summary>
    public partial class ShoppingCartPage : Page
    {
        public ShoppingCartPage(ShoppingCartViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
