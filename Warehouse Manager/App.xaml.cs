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

namespace Warehouse_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IServiceProvider serviceProvider = CreateServiceProvider();
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }
        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(s => new MainWindow(
                s.GetRequiredService<IAuthenticator>(),
                s.GetRequiredService<IProductService>()
            ));

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer("Data Source=DESKTOP-7RLU0G4;Initial Catalog=warehouse;Integrated Security=True"));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<ShoppingCartViewModel>();

            services.AddScoped<RegisterPage>();
            services.AddScoped<HomePage>();
            services.AddScoped<ProductPage>();
            services.AddScoped<AddProductPage>();
            services.AddScoped<UpdateProductPage>();
            services.AddScoped<Login>();
            services.AddScoped<DeleteProductConfirmationPage>();
            services.AddScoped<ShoppingCartView>();

            return services.BuildServiceProvider();
        }
    }
}