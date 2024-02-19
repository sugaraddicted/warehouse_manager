using Microsoft.Extensions.DependencyInjection;
using System;
using Warehouse_Manager.MVVM.View;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public static class ViewModelFactory
    {
        private static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static object CreateViewModel(Type pageType)
        {
            return pageType switch
            {
                Type t when t == typeof(HomeViewModel) => _serviceProvider.GetService<HomeViewModel>(),
                Type t when t == typeof(OrderViewModel) => _serviceProvider.GetService<OrderViewModel>(),
                Type t when t == typeof(AddProductViewModel) => _serviceProvider.GetService<AddProductViewModel>(),
                Type t when t == typeof(ShoppingCartViewModel) => _serviceProvider.GetService<ShoppingCartViewModel>(),
                Type t when t == typeof(RegisterViewModel) => _serviceProvider.GetService<RegisterViewModel>(),
                Type t when t == typeof(LoginViewModel) => _serviceProvider.GetService<LoginViewModel>(),
                Type t when t == typeof(OrdersViewModel) => _serviceProvider.GetService<OrdersViewModel>(),
                // Add more cases for other page types
                _ => throw new ArgumentException($"Unsupported page type: {pageType.Name}", nameof(pageType))
            };
        }
    }
}
