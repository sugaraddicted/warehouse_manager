using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class RegisterViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IAuthenticator _authenticator;
        public RelayCommand BackButtonCommand { get; private set; }
        public RelayCommand RegisterButtonCommand { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _username;
        public string Username
        {
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

        private string _firstname;
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

        private string _lastname;
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

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
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

        private string _passwordConfirmation;

        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set
            {
                if (_passwordConfirmation != value)
                {
                    _passwordConfirmation = value;
                    OnPropertyChanged(nameof(PasswordConfirmation));
                }
            }
        }

        public RegisterViewModel(IAuthenticator authenticator)
        {
            BackButtonCommand = new RelayCommand(NavigateToLoginPage);
            RegisterButtonCommand = new RelayCommand(Register);
            _authenticator = authenticator;
        }

        private async void Register()
        {
            var registerDto = new RegisterDto();

            registerDto.Username = Username;
            registerDto.Firstname = Firstname;
            registerDto.Lastname = Lastname;
            registerDto.Email = Email;
            registerDto.Password = Password;
            registerDto.PasswordConfirmation = PasswordConfirmation;

            await _authenticator.Register(registerDto, "NotAdmin");

        }

        private void NavigateToLoginPage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                var viewModel = (LoginViewModel)ViewModelFactory.CreateViewModel(typeof(LoginViewModel));
                frame.Navigate(new LoginPage(viewModel));
            }
        }
    }
}
