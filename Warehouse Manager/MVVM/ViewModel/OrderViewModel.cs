using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.State.Authenticators;
using Warehouse_Manager.MVVM.View;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthenticator _authenticator;
        private readonly IShippingAddressService _shippingAddressService;

        private List<ShoppingCartItem> _cartItems;
        private decimal Total { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand BackButtonCommand { get; private set; }
        public RelayCommand PlaceOrderButtonCommand { get; private set; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderViewModel(IAuthenticator authenticator, IOrderService orderService, IShippingAddressService shippingAddressService)
        {
            _authenticator = authenticator;
            _orderService = orderService;
            _shippingAddressService = shippingAddressService;

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
            Total = _authenticator.ShoppingCart.GetShoppingCartTotal();

            BackButtonCommand = new RelayCommand(NavigateToShoppingCartPage);
            PlaceOrderButtonCommand = new RelayCommand(PlaceOrder);
        }
        private string _notes;
        private string _firstname;
        private string _lastname;
        private string _phoneNumber;
        private string _region;
        private string _street;
        private string _city;
        private string _zipCode;
        private int _building;

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

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        public string Region
        {
            get { return _region; }
            set
            {
                if (_region != value)
                {
                    _region = value;
                    OnPropertyChanged(nameof(Region));
                }
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                if (_street != value)
                {
                    _street = value;
                    OnPropertyChanged(nameof(Street));
                }
            }
        } 
        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (_zipCode != value)
                {
                    _zipCode = value;
                    OnPropertyChanged(nameof(ZipCode));
                }
            }
        } 
        public int Building
        {
            get { return _building; }
            set
            {
                if (_building != value)
                {
                    _building = value;
                    OnPropertyChanged(nameof(Building));
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
            List<OrderItem> orderItems = new List<OrderItem>();

            // Populate order items
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
            order.Total = (double)Total;

            await _orderService.StoreOrderAsync(order);
            ShippingAddress shippingAddress = new ShippingAddress()
            {
                FirstName = Firstname,
                LastName = Lastname,
                PhoneNumber = PhoneNumber,
                Region = Region,
                City = City,
                Street = Street,
                ZipCode = ZipCode,
                Building = Building,
                OrderId = order.Id
            };

            await _shippingAddressService.AddAsync(shippingAddress);
            order.ShippingAddressId = shippingAddress.Id;
            await _orderService.UpdateAsync(order.Id, order);
        }

        private void NavigateToShoppingCartPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (ShoppingCartViewModel)ViewModelFactory.CreateViewModel(typeof(ShoppingCartViewModel));
                frame.Navigate(new ShoppingCartPage(viewModel));
            }
        }
    }
}
