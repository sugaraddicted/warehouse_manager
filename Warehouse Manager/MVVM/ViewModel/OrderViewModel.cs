using MaterialDesignThemes.Wpf;
using System;
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
    public class OrderViewModel
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IAuthenticator _authenticator;

        private List<ShoppingCartItem> _cartItems;
        private ShippingAddress _shippingAddress;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderViewModel(IProductService productService, IAuthenticator authenticator, IOrderService orderService)
        {
            _productService = productService;
            _authenticator = authenticator;
            _orderService = orderService;

            var cartItems = _authenticator.ShoppingCart.GetShoppingCartItems();
            foreach (var cartItem in cartItems)
            {
                if (cartItem.Product == null || cartItem.Quantity <= 0)
                {
                    _authenticator.ShoppingCart.ShoppingCartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Product.SetImageSource();
                }
            }
            CartItems = cartItems;  
        }
        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }

        public ShippingAddress ShippingAddress
        {
            get { return _shippingAddress; }
            set
            {
                if (_shippingAddress != value)
                {
                    _shippingAddress = value;
                    OnPropertyChanged(nameof(ShippingAddress));
                }
            }
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

        public async void PlaceOrder()
        {
            Order order = new Order();
            order.ShippingAddress = _shippingAddress;
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };
                orderItems.Add(orderItem);
            }
            order.OrderItems = orderItems;
            order.OrderDate = DateTime.Now;
            order.UserId = _authenticator.CurrentUser.Id;
            order.CustomerNotes = Notes;
            await _orderService.StoreOrderAsync(order);

        }
        private void NavigateToShoppingCartPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new ShoppingCartView(_productService, _authenticator));
            }
        }
    }
}
