using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.View;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class OrderDetailsViewModel : ViewModelBase
    {
        private readonly IOrderService _orderService;
        private Order Order { get; set; }
        public decimal Total { get; set; }
        public string Notes { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int Building { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public RelayCommand BackButtonCommand { get; private set; }

        public OrderDetailsViewModel(IOrderService orderService, int orderId)
        {
            _orderService = orderService;
            GetOrder(orderId);

            BackButtonCommand = new RelayCommand(NavigateToOrdersPage);
        }
        private void NavigateToOrdersPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (OrdersViewModel)ViewModelFactory.CreateViewModel(typeof(OrdersViewModel));
                frame.Navigate(new OrdersPage(viewModel));
            }
        }

        private void GetOrder(int orderId)
        {
            Order = _orderService.GetOrderById(orderId);
            OrderItems = Order.OrderItems;

            Total = Order.Total;

            Notes = Order.CustomerNotes;
            Lastname = Order.ShippingAddress.LastName;
            Firstname = Order.ShippingAddress.FirstName;
            Region = Order.ShippingAddress.Region;
            City = Order.ShippingAddress.City;
            Street = Order.ShippingAddress.Street;
            PhoneNumber = Order.ShippingAddress.PhoneNumber;
            ZipCode = Order.ShippingAddress.ZipCode;
            Building = Order.ShippingAddress.Building;
        }
    }
}
