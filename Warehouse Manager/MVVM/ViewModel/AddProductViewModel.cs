using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Views;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class AddProductViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IProductService _productService;
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
        public AddProductViewModel(IProductService productService)
        {
            _productService = productService;
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
                var viewModel = (HomeViewModel)ViewModelFactory.CreateViewModel(typeof(HomeViewModel));
                frame.Navigate(new HomePage(viewModel));
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
