using System.Windows.Controls;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;
using System.ComponentModel;
using Warehouse_Manager.Data.Services.Interfaces;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticator _authenticator;
        private readonly IProductService _productService;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _username;
        public string Username {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public RelayCommand LoginButtonCommand { get; private set; }
        public RelayCommand SignupButtonCommand { get; private set; }
        public RelayCommand ContinueButtonCommand { get; private set; }

        public LoginViewModel(IAuthenticator authenticator, IProductService productService)
        {
            _authenticator = authenticator;
            _productService = productService;
            SignupButtonCommand = new RelayCommand(NavigateToRegisterPage);
            ContinueButtonCommand = new RelayCommand(NavigateToHomePage);
            LoginButtonCommand = new RelayCommand(Login);
        }

        private void NavigateToRegisterPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new RegisterPage(_authenticator, _productService));
            }
        }

        private void NavigateToHomePage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new HomePage(_productService, _authenticator));
            }
        }

        private async void Login()
        {
            await _authenticator.Login(Username, Password);
            NavigateToHomePage();
        }
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
