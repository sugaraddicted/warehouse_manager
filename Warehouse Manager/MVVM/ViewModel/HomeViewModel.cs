using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;
        public RelayCommand AddProductButtonCommand { get; private set; }
        public RelayCommand CartButtonCommand { get; set; }
        public ICommand DetailsButtonCommand { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<ProductDto> _products = new List<ProductDto>();
        public List<ProductDto> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public HomeViewModel(IProductService productService, IAuthenticator authenticator)
        {
            _productService = productService;
            _authenticator = authenticator;
            DetailsButtonCommand = new RelayCommand<ProductDto>(NavigateToDetailsPage);
            AddProductButtonCommand = new RelayCommand(NavigateToAddProductPage);
            CartButtonCommand = new RelayCommand(NavigateToShoppingCartPage);
        }

        private void NavigateToShoppingCartPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new ShoppingCartView(_productService, _authenticator));
            }
        }

        private void NavigateToDetailsPage(ProductDto productVM)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new ProductPage(_productService, productVM, _authenticator));
            }
        }

        private void NavigateToAddProductPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new AddProductPage(_productService, _authenticator));
            } 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetProducts()
        {
            var products = await _productService.GetAllAsync();
            foreach (var item in products)
            {
                var productViewModel = new ProductDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    StockQuantity = item.StockQuantity,
                    BinaryContent = item.BinaryContent,
                    FileType = item.FileType
                };
                productViewModel.SetImageSource();
                Products.Add(productViewModel);
            }
        }
    }
}
