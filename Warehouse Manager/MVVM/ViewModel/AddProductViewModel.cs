using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IAuthenticator _authenticator;
        public ProductDto productViewModel { get; set; } = new ProductDto();
        public RelayCommand BackButtonCommand { get; private set; }
        public RelayCommand UploadImageButtonCommand { get; private set; }
        public RelayCommand AddProuctButtonCommand { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public AddProductViewModel(IProductService productService, IAuthenticator authenticator)
        {
            _productService = productService;
            _authenticator = authenticator;
            BackButtonCommand = new RelayCommand(NavigateToAdminHomePage);
            UploadImageButtonCommand = new RelayCommand(UploadImage);
            AddProuctButtonCommand = new RelayCommand(AddProduct);
        }

        private async void AddProduct()
        {
            var newProduct = new ProductDto
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                StockQuantity = productViewModel.StockQuantity,
                BinaryContent = Image
            };

            try
            {
                await _productService.AddNewProductAsync(newProduct);
                NavigateToAdminHomePage();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding product: {ex.Message}");
            }

        }

        private void NavigateToAdminHomePage()
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new HomePage(_productService, _authenticator));
            }
        }
        private void UploadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                ImageSource imageSource = Helper.ConvertByteArrayToImage(imageData);
                ImageSource = imageSource;
                Image = imageData;
            }
        }
    }
}
