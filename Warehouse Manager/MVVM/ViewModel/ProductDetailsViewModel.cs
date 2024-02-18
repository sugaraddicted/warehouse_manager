using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class ProductDetailsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserRole { get; set; }
        public decimal Price { get; set; }
        public ImageSource Image { get; set; }
        public ProductDto Product { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand BackButtonCommand { get; private set; }
        public ICommand UpdateButtonCommand { get; private set; }
        public ICommand DeleteButtonCommand { get; private set; }
        public ICommand AddToCartButtonCommand { get; private set; }

        public ProductDetailsViewModel(IProductService productService, ProductDto productVM, IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _productService = productService;

            if (_authenticator.CurrentUser == null)
            {
                UserRole = "Unauthorized";
            }
            else
            {
                UserRole = _authenticator.CurrentUser.UserRole;
            }

            Product = productVM;
            Name = productVM.Name;
            Description = productVM.Description;
            Price = productVM.Price;
            Image = Helper.ConvertByteArrayToImage(productVM.BinaryContent);
            BackButtonCommand = new RelayCommand(NavigateToHomePage);
            UpdateButtonCommand = new RelayCommand<ProductDto>(NavigateToUpdetePage);
            DeleteButtonCommand = new RelayCommand<ProductDto>(NavigateToDeletePage);
            AddToCartButtonCommand = new RelayCommand(AddToShoppingCart);
        }

        private void NavigateToDeletePage(ProductDto model)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = new DeleteProductViewModel(_productService, model, _authenticator);
                frame.Navigate(new DeleteProductConfirmationPage(viewModel));
            }
        }

        private void NavigateToUpdetePage(ProductDto model)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = new UpdateProductViewModel(_productService, model, _authenticator);
                frame.Navigate(new UpdateProductPage(viewModel));
            }
        }

        private void NavigateToHomePage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (HomeViewModel)ViewModelFactory.CreateViewModel(typeof(HomeViewModel));
                frame.Navigate(new HomePage(viewModel));
            }
        }

        private async void AddToShoppingCart()
        {
            var product = await _productService.GetByIdAsync(Product.Id);
            if (product != null)
            {
                _authenticator.ShoppingCart.AddItemToCart(product);
            }
        }
    }
}
