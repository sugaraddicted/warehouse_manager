using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class DeleteProductViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;

        public ProductDto Product { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand DeleteButtonCommand { get; private set; }
        public ICommand BackButtonCommand { get; private set; }
        public DeleteProductViewModel(IProductService productService, ProductDto productVM, IAuthenticator authenticator)
        {
            _productService = productService;
            Product = productVM;
            _authenticator = authenticator;
            BackButtonCommand = new RelayCommand<ProductDto>(NavigateToDetailsPage);
            DeleteButtonCommand = new RelayCommand(DeleteProduct);
        }

        private void NavigateToDetailsPage(ProductDto productVM)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = new ProductDetailsViewModel(_productService, productVM, _authenticator);
                frame.Navigate(new ProductPage(viewModel));
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
        private async void DeleteProduct()
        {
            await _productService.DeleteAsync(Product.Id);
            NavigateToHomePage();
        }
        
    }
}
