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
    /// Логика взаимодействия для LookProductPage.xaml
    /// </summary>
    public partial class LookProductPage : Page
    {
        Product contextProduct;
        public LookProductPage(Product product)
        {
            InitializeComponent();
            contextProduct = product;
            DataContext = contextProduct;
            Refresh();

        }
        private void Refresh()
        {
            LVPhoto.ItemsSource = contextProduct.ProductPhoto.ToList();
        }
        private void BackBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BucketBt_Click(object sender, RoutedEventArgs e)
        {
            //Добавление товара в коризну
            try
            {
                var selectedProduct = (sender as Button).DataContext as Product;
                if (selectedProduct.Count > 0)
                {
                    Bucket bucket = new Bucket
                    {
                        Quantity = 1,
                        UserId = Account.AuthUser.Id,
                        ProductId = selectedProduct.Id
                    };

                    var prodInBucket = App.db.Bucket.Where(b => b.ProductId == bucket.ProductId).FirstOrDefault();
                    if (prodInBucket != null) { MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); return; };

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
    }
}
