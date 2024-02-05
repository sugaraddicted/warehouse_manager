using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Warehouse_Manager.Data;
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.View;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.ViewModel
{
    public class UpdateProductViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticator _authenticator;
        private readonly IProductService _productService;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        private int _stockQuantity;
        public int StockQuantity
        {
            get { return _stockQuantity; }
            set
            {
                if (_stockQuantity != value)
                {
                    _stockQuantity = value;
                    OnPropertyChanged(nameof(StockQuantity));
                }
            }
        }

        private byte[] _binaryContent;
        public byte[] BinaryContent
        {
            get { return _binaryContent; }
            set
            {
                _binaryContent = value;
                OnPropertyChanged(nameof(BinaryContent));
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public ProductDto Product { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand BackButtonCommand { get; private set; }
        public ICommand UpdateButtonCommand { get; private set; }
        public RelayCommand UploadImageButtonCommand { get; private set; }

        public UpdateProductViewModel(IProductService productService, ProductDto productVM)
        {
            Name = productVM.Name;
            Description = productVM.Description;
            Price = productVM.Price;
            Image = productVM.Image;
            StockQuantity = productVM.StockQuantity;
            Product = productVM;
            Image = Helper.ConvertByteArrayToImage(productVM.BinaryContent);
            BackButtonCommand = new RelayCommand<ProductDto>(NavigateToDetailsPage);
            UploadImageButtonCommand = new RelayCommand(UploadImage);
            UpdateButtonCommand = new RelayCommand(UpdateProduct);
            _productService = productService;
        }

        private void NavigateToDetailsPage(ProductDto productVM)
        {
            if (Application.Current.MainWindow.FindName("MainFrame") is Frame frame)
            {
                frame.Navigate(new ProductPage(_productService, productVM, _authenticator));
            }
        }

        private async void UpdateProduct()
        {
            Product.Name = Name;
            Product.Description = Description;
            Product.Price = Price;
            Product.Image = Image;
            Product.StockQuantity = StockQuantity;
            await _productService.UpdateProductAsync(Product);
            NavigateToDetailsPage(Product);
        }

        private void UploadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                ImageSource imageSource = Helper.ConvertByteArrayToImage(imageData);
                Image = imageSource;
                BinaryContent = imageData;
                Product.BinaryContent = BinaryContent;
            }
        }
    }
}
