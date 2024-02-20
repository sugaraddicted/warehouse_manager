using System;
using System.Windows;
using Warehouse_Manager.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Warehouse_Manager.Data.Services.AuthenticationServices;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.Data.Services;
using Warehouse_Manager.MVVM.ViewModel;
using Warehouse_Manager.State.Authenticators;
using Warehouse_Manager.MVVM.View;
using Microsoft.EntityFrameworkCore;
using Warehouse_Manager.Data.Services.Interfaces;
using System.Windows.Controls;

namespace Warehouse_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IServiceProvider serviceProvider = CreateServiceProvider();
            ViewModelFactory.Initialize(serviceProvider);
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }

        [STAThread]
        private IServiceProvider CreateServiceProvider()
        {

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(s => new MainWindow(
                s.GetRequiredService<IAuthenticator>()
            ));
            services.AddSingleton<Frame>();

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer("Data Source=DESKTOP-7RLU0G4;Initial Catalog=warehouse;Integrated Security=True"));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShippingAddressService, ShippingAddressService>();
            services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddTransient<ShoppingCartViewModel>();
            services.AddScoped<LoginViewModel>();

            services.AddScoped<RegisterViewModel>();
            services.AddTransient<AddProductViewModel>();
            services.AddTransient<DeleteProductViewModel>();
            services.AddTransient<UpdateProductViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<OrderViewModel>();
            services.AddTransient<OrdersViewModel>();
            services.AddTransient<ProductDetailsViewModel>();
            services.AddTransient<OrderDetailsViewModel>();

            services.AddScoped<RegisterPage>();
            services.AddTransient<HomePage>();
            services.AddTransient<ProductPage>();
            services.AddTransient<AddProductPage>();
            services.AddTransient<UpdateProductPage>();
            services.AddScoped<LoginPage>();
            services.AddTransient<DeleteProductConfirmationPage>();
            services.AddTransient<OrdersPage>();
            services.AddTransient<OrderDetailsPage>();

            return services.BuildServiceProvider();
        }
    }
}