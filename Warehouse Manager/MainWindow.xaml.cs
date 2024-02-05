using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse_Manager.MVVM.ViewModel;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;
using Warehouse_Manager.Data.Services.AuthenticationServices;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.Data;
using Microsoft.AspNetCore.Identity;
using Warehouse_Manager.Data.Services.Interfaces;

namespace Warehouse_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Window;
        private readonly IAuthenticator _authenticator;
        private readonly IProductService _productService;
        public MainWindow()
        {
            InitializeComponent();
            Hide();
        }
        public MainWindow(IAuthenticator authenticator, IProductService productService)
        {
            _authenticator = authenticator;
            _productService = productService;
            InitializeComponent();
            Window = this;
            MainFrame.Content = new Login(_authenticator, _productService);
        }

        private void Move(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                MainWindow.Window.DragMove();
            }
        }
    }
}
