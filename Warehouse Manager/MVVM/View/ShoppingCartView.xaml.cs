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
    /// Interaction logic for ShoppingCartView.xaml
    /// </summary>
    public partial class ShoppingCartView : Page
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;
        public ShoppingCartView(IProductService productService, IAuthenticator authenticator)
        {
            InitializeComponent();
            _productService = productService;
            _authenticator = authenticator;
            var viewModel = new ShoppingCartViewModel(_productService, _authenticator);
            DataContext = viewModel;
        }
    }
}
