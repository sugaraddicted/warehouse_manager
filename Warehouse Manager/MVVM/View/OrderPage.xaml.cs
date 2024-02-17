using System.Windows.Controls;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage(OrderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
