using System.Windows.Controls;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using System.Drawing;
using System;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IAuthenticator _authenticator;

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

        public LoginViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            SignupButtonCommand = new RelayCommand(NavigateToRegisterPage);
            ContinueButtonCommand = new RelayCommand(NavigateToHomePage);
            LoginButtonCommand = new RelayCommand(Login);
        }

        private void NavigateToRegisterPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (RegisterViewModel)ViewModelFactory.CreateViewModel(typeof(RegisterViewModel));
                frame.Navigate(new RegisterPage(viewModel));
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

        private async void Login()
        {
            var result = await _authenticator.Login(Username, Password);

            if (result)
            {
                NavigateToHomePage();
            }
            else
            {
                MessageBox.Show("Invalid cradentials, try again");
            }
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
