using System.Windows.Controls;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage(OrdersViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
