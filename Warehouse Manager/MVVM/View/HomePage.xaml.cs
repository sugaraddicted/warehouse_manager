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
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.ViewModel;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly IAuthenticator _authenticator;
        private readonly IProductService _productService;

        public HomePage(IProductService productService, IAuthenticator authenticator)
        {
            _productService = productService;
            _authenticator = authenticator;
            InitializeComponent();
            InitializeDataContext();
        }

        private async void InitializeDataContext()
        {
            var viewModel = new HomeViewModel(_productService, _authenticator);
            await viewModel.GetProducts();
            DataContext = viewModel;
        }
    }
}
