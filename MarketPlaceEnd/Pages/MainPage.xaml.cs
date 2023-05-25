﻿using MarketPlaceEnd.Models;
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

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        

        private void BucketBt_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AccountBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.NavigationService.Navigate(new PersAccountPage());

        }

        private void ProvBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.NavigationService.Navigate(new ProvidersPage());

        }

        private void ProductsBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.NavigationService.Navigate(new ProductsPage());
        }
    }
}