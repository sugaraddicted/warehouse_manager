using System.Windows.Controls;
using Warehouse_Manager.MVVM.ViewModel;

namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for OrderDetailsPage.xaml
    /// </summary>
    public partial class OrderDetailsPage : Page
    {
        public OrderDetailsPage(OrderDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
