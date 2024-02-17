using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class HomeViewModel : ViewModelBase, INotifyPropertyChanged
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

            GetProducts();

            DetailsButtonCommand = new RelayCommand<ProductDto>(NavigateToDetailsPage);
            AddProductButtonCommand = new RelayCommand(NavigateToAddProductPage);
            CartButtonCommand = new RelayCommand(NavigateToShoppingCartPage);
        }

        private void NavigateToShoppingCartPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (ShoppingCartViewModel)ViewModelFactory.CreateViewModel(typeof(ShoppingCartViewModel));
                frame.Navigate(new ShoppingCartPage(viewModel));
            }
        }

        private void NavigateToDetailsPage(ProductDto productVM)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = new ProductDetailsViewModel(_productService, productVM, _authenticator);
                frame.Navigate(new ProductPage(viewModel));
            }
        }

        private void NavigateToAddProductPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (AddProductViewModel)ViewModelFactory.CreateViewModel(typeof(AddProductViewModel));
                frame.Navigate(new AddProductPage(viewModel));
            } 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void GetProducts()
        {
            var products = await _productService.GetAllAsync();
            var result = new List<ProductDto>();
            foreach (var item in products)
            {
                var productDto = new ProductDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    StockQuantity = item.StockQuantity,
                    BinaryContent = item.BinaryContent,
                    FileType = item.FileType
                };
                productDto.SetImageSource();
                result.Add(productDto);
            }

            Products = result;
        }
    }
}
