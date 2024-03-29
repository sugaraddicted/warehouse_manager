﻿using System;
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
using Warehouse_Manager.Data.Services.Interfaces;
using Warehouse_Manager.Dto;
using Warehouse_Manager.MVVM.ViewModel;
using Warehouse_Manager.State.Authenticators;

namespace Warehouse_Manager.MVVM.View
{
    /// <summary>
    /// Interaction logic for UpdateProductPage.xaml
    /// </summary>
    public partial class UpdateProductPage : Page
    {
        public UpdateProductPage(UpdateProductViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
