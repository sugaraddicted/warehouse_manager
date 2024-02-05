using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;

        public string Name { get; set; }
        public string Description { get; set; }
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
                frame.Navigate(new DeleteProductConfirmationPage(_productService, model, _authenticator)) ;
            }
        }

        private void NavigateToUpdetePage(ProductDto model)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new UpdateProductPage(model, _productService));
            }
        }

        private void NavigateToHomePage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new HomePage(_productService, _authenticator));
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
