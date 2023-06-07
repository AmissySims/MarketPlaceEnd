using MarketPlaceEnd.Models;
using MarketPlaceEnd.Pages.AddEditPages;
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
    /// Логика взаимодействия для TypeProductsPage.xaml
    /// </summary>
    public partial class TypeProductsPage : Page
    {
 
        public TypeProductsPage()
        {
            InitializeComponent();
            
            if (Account.AuthUser.RoleId == 1)
            {
                AddTypeBt.Visibility = Visibility.Visible;
            }
            else
            {
                AddTypeBt.Visibility = Visibility.Collapsed;
            }


        }

     
        public void Refresh()
        {
            
            var listType = App.db.TypeProduct.ToList();
            var searchString = FoundTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listType = listType.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                TypesList.ItemsSource = listType;
            }
            TypesList.ItemsSource = listType;
        }
        private void AddTypeBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditTypesPage(new TypeProduct()));
        }

        private void DeleteTypeBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selType = (sender as Button).DataContext as TypeProduct;
                var pr = selType.Product;
                App.db.Product.RemoveRange(pr);
                App.db.TypeProduct.Remove(selType);
                App.db.SaveChanges();
                MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void EditTypeBt_Click(object sender, RoutedEventArgs e)
        {
            var selType = (sender as Button).DataContext as TypeProduct;
            NavigationService.Navigate(new AddEditTypesPage(selType));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
