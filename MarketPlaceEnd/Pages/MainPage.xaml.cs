using MarketPlaceEnd.Models;
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
            Frame2.NavigationService.Navigate(new MainMainPage());
            if (Account.AuthUser.RoleId == 3)
            {
                ProvBt.Visibility = Visibility.Collapsed;
                TypesBt.Visibility = Visibility.Collapsed;
                DeliveryBt.Visibility = Visibility.Visible;
            }
            else if (Account.AuthUser.RoleId == 4 || Account.AuthUser.RoleId == 5 || Account.AuthUser.RoleId == 2)
            {
                ProvBt.Visibility = Visibility.Collapsed;
                TypesBt.Visibility = Visibility.Collapsed;
                DeliveryBt.Visibility = Visibility.Collapsed;
                BucketBt.Visibility = Visibility.Collapsed;


            }
          
            else
            {
                ProvBt.Visibility = Visibility.Visible;
                TypesBt.Visibility = Visibility.Visible;
                DeliveryBt.Visibility = Visibility.Visible;
            }
        }

        

        private void BucketBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.NavigationService.Navigate(new BusketPage());
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

        private void TypesBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.NavigationService.Navigate(new TypeProductsPage());
        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void DeliveryBt_Click(object sender, RoutedEventArgs e)
        {

            Frame2.NavigationService.Navigate(new DeliveryPountsPage());
        }

        private void MainBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.NavigationService.Navigate(new MainMainPage());
        }
    }
}
