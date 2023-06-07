using MarketPlaceEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MarketPlaceEnd.Pages.AddEditPages;

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProvidersPage.xaml
    /// </summary>
    public partial class ProvidersPage : Page
    {
        
        public ProvidersPage()
        {
            InitializeComponent();
            
            if(Account.AuthUser.RoleId == 1)
            {
                AddProvBt.Visibility = Visibility.Visible;
            }
            else
            {
                AddProvBt.Visibility = Visibility.Collapsed;
            }
        }

        
        public void Refresh()
        {
                var listProv = App.db.Provider.ToList();
                var searchString = FoundTb.Text;
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    listProv = listProv.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                    ProvidersList.ItemsSource = listProv;
                }
                ProvidersList.ItemsSource = listProv;
            
        }
        private void AddProvBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProviderPage(new Provider()));
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void EditProvBt_Click(object sender, RoutedEventArgs e)
        {
            var selProv = (sender as Button).DataContext as Provider;
            NavigationService.Navigate(new AddEditProviderPage(selProv));
        }

        private void DeleteProveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selProv = (sender as Button).DataContext as Provider;
                var pr = selProv.Product;
                App.db.Product.RemoveRange(pr);
                App.db.Provider.Remove(selProv);
                App.db.SaveChanges();
                MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
         

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
