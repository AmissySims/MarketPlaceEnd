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
        TypeProduct selType;
        public ProductsPage()
        {
            InitializeComponent();

            //List<TypeProduct> listTypes = _context.TypeProduct.ToList();

            //listTypes.Insert(0, new TypeProduct { Title = "Все" });

            //TypeProdCb.ItemsSource = listTypes;
            // TypeProdCb.SelectedIndex = 0;
            selType = null;
            if (Account.AuthUser.RoleId == 1)
            {
                AddPrBt.Visibility = Visibility.Visible;
            }
            else
            {
                AddPrBt.Visibility = Visibility.Collapsed;
            }
            FilterCb.Items.Add("Все");
            FilterCb.Items.Add("от 0 до 500 руб.");
            FilterCb.Items.Add("от 500 до 1500 руб.");
            FilterCb.Items.Add("от 1500 руб.");


        }
        private void Refresh()
        {
            var products = App.db.Product.ToList();

            var searchString = FoundTb.Text;
            if (selType != null)
            {
                products = products.Where(x => x.TypeProductId == selType.Id).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = App.db.Product.Where(p => p.Title.ToLower().Contains(searchString)).ToList();
            }
            if (FilterCb.SelectedIndex == 1)
            {
                products = products.Where(p => p.Price < 500).ToList();
            }
            if (FilterCb.SelectedIndex == 2)
            {
                products = products.Where(p => p.Price >= 500 && p.Price < 2000).ToList();
            }
            if (FilterCb.SelectedIndex == 3)
            {
                products = products.Where(p => p.Price >= 2000).ToList();
            }
            ListProducts.ItemsSource = products;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void AddInBucketBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Добавление товара в коризну
                var selectedProduct = (sender as Button).DataContext as Product;
                if (selectedProduct.Count > 0)
                {
                    Bucket bucket = new Bucket
                    {
                        Quantity = 1,
                        UserId = Account.AuthUser.Id,
                        ProductId = selectedProduct.Id
                    };

                    var prodInBucket = App.db.Bucket.Where(b => b.ProductId == bucket.ProductId && b.UserId == Account.AuthUser.Id).FirstOrDefault();
                    if (prodInBucket != null)
                    {
                        MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    App.db.Bucket.Add(bucket);
                    App.db.SaveChanges();
                    MessageBoxResult result = MessageBox.Show("Товар добавлен в корзину. Хотите перейти в корзину сейчас?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    // Если пользователь выбрал "Да", перейти на вкладку корзины
                    if (result == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new BusketPage());
                    }

                }
                else
                {
                    MessageBox.Show("Товара нет в наличии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
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
            Refresh();
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
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



        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void ElectBt_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.TypeProduct.FirstOrDefault(x => x.Id == 1);
            Refresh();

        }


        private void ClothesBt_Click(object sender, RoutedEventArgs e)
        {

            selType = App.db.TypeProduct.FirstOrDefault(x => x.Id == 2);
            Refresh();

        }

        private void AllBt_Click(object sender, RoutedEventArgs e)
        {
            selType = null;
            Refresh();

        }

        private void CosmeticBt_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.TypeProduct.FirstOrDefault(x => x.Id == 3);
            Refresh();

        }


        private void SportBt_Click(object sender, RoutedEventArgs e)
        {

            selType = App.db.TypeProduct.FirstOrDefault(x => x.Id == 5);
            Refresh();
        }

        private void FoodBt_Click(object sender, RoutedEventArgs e)
        {

            selType = App.db.TypeProduct.FirstOrDefault(x => x.Id == 4);
            Refresh();
        }


    }
}
            
