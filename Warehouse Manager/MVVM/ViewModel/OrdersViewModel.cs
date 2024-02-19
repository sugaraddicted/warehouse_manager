using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticator _authenticator;
        private readonly IOrderService _orderService;

        private List<Order> _orders { get; set; }
        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public RelayCommand BackButtonCommand { get; private set; }

        public OrdersViewModel(IAuthenticator authenticator, IOrderService orderService)
        {
            _authenticator = authenticator;
            _orderService = orderService;
            GetOrders();
            BackButtonCommand = new RelayCommand(NavigateToMainPage);
        }

        private void NavigateToMainPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (HomeViewModel)ViewModelFactory.CreateViewModel(typeof(HomeViewModel));
                frame.Navigate(new HomePage(viewModel));
            }
        }

        public async void GetOrders()
        {
            if (_authenticator.CurrentUser.UserRole == "admin")
            {
                var result = await _orderService.GetAllAsync();
                Orders = result.ToList();
            }
            else
            {
                var result = await _orderService.GetAllOrdersByUserIdAsync(_authenticator.CurrentUser.Id);
                Orders = result.ToList();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
