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
        MarketPlaceEntities _context = new MarketPlaceEntities();
        public TypeProductsPage()
        {
            InitializeComponent();
            Sort();
           
           
        }

        public void Sort()
        {
            List<TypeProduct> listType = _context.TypeProduct.ToList();
            var searchString = FoundTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listType = listType.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                TypesList.ItemsSource = listType;
            }
            TypesList.ItemsSource = listType;
        }
        public void Refresh()
        {
            TypesList.ItemsSource = App.db.TypeProduct.ToList();
        }
        private void AddTypeBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditTypesPage(new TypeProduct()));
        }

        private void DeleteTypeBt_Click(object sender, RoutedEventArgs e)
        {
            var selType = (sender as Button).DataContext as TypeProduct;
            var pr = selType.Product;
            App.db.Product.RemoveRange(pr);
            App.db.TypeProduct.Remove(selType);
            App.db.SaveChanges();
            MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
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
            Sort();
        }
    }
}
