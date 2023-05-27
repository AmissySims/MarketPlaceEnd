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
using MarketPlaceEnd.Pages.AddEditPages;
using System.Data.Entity.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        MarketPlaceEntities _context = new MarketPlaceEntities();

       
        public ProductsPage()
        {
            InitializeComponent();
            List<TypeProduct> listTypes = _context.TypeProduct.ToList();

            listTypes.Insert(0, new TypeProduct { Title = "Все" });

           TypeProdCb.ItemsSource = listTypes;
            TypeProdCb.SelectedIndex = 0;
            Sort();

        }
         private void Refresh()
         {
            ListProducts.ItemsSource = App.db.Product.ToList();
         }
        private void Sort()
        {
            List<Product> listProd = _context.Product.ToList();

            if (TypeProdCb.SelectedIndex != 0)
            {
                TypeProduct selProd = (TypeProduct)TypeProdCb.SelectedItem;
                listProd = listProd.Where(x => x.TypeProductId == selProd.Id).ToList();
            }
            var searchString = FoundTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listProd = listProd.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                ListProducts.ItemsSource = listProd;
            }
           
            ListProducts.ItemsSource = listProd;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void RemovePrBt_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Product;
            App.db.Product.Remove(selProd);
            App.db.SaveChanges();
            MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        private void EditPrPrBt_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Product;
            NavigationService.Navigate(new AddEditProductPage(selProd));
        }

        private void TypeProdCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }

        private void AddPrBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage(new Product()));

        }

        private void LookPrBt_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Product;
            NavigationService.Navigate(new LookProductPage(selProd));
        }

        private void AddInBucketBt_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
