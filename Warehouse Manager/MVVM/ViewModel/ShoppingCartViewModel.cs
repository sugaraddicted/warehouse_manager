using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;

        private List<ShoppingCartItem> _cartItems;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ShoppingCartItem> CartItems
        {
            get { return _cartItems; }
            set
            {
                if (_cartItems != value)
                {
                    _cartItems = value;
                    OnPropertyChanged(nameof(CartItems));
                }
            }
        }

        public RelayCommand BackButtonCommand { get; private set; }
        public RelayCommand<ShoppingCartItem> DecreaseQuantityCommand { get; private set; }
        public RelayCommand<ShoppingCartItem> IncreaseQuantityCommand { get; private set; }
        public ShoppingCartViewModel(IProductService productService, IAuthenticator authenticator)
        {
            _productService = productService;
            _authenticator = authenticator;
            CartItems = new List<ShoppingCartItem>();
            UpdateItems();

            BackButtonCommand = new RelayCommand(NavigateToMainPage);
            DecreaseQuantityCommand = new RelayCommand<ShoppingCartItem>(DecreaseQuantity);
            IncreaseQuantityCommand = new RelayCommand<ShoppingCartItem>(IncreaseQuantity);
        }

        private void NavigateToMainPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new HomePage(_productService, _authenticator));
            }
        }

        public void DecreaseQuantity(ShoppingCartItem cartItem)
        {
            _authenticator.ShoppingCart.RemoveItemFromCart(cartItem.Product);
            UpdateItems();
        }

        public void IncreaseQuantity(ShoppingCartItem cartItem)
        {
            _authenticator.ShoppingCart.AddItemToCart(cartItem.Product);
            UpdateItems();
        }
        public void UpdateItems()
        {
            try
            {
                var cartItems = _authenticator.ShoppingCart.GetShoppingCartItems();
                foreach (var cartItem in cartItems)
                {
                    if(cartItem.Product == null || cartItem.Quantity <= 0)
                    {
                        _authenticator.ShoppingCart.ShoppingCartItems.Remove(cartItem);
                    }
                    else {
                        cartItem.Product.SetImageSource();
                    }
                }
                CartItems = cartItems;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
