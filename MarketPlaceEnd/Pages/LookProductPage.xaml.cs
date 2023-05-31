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
                Bucket bucket = new Bucket
                {
                    Quantity = (int)selectedProduct.Count,
                    UserId = Account.AuthUser.Id,
                    ProductId = selectedProduct.Id
                };
                App.db.Bucket.Add(bucket);
                App.db.SaveChanges();
                MessageBoxResult result = MessageBox.Show("Товар добавлен в корзину. Хотите перейти в корзину сейчас?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Если пользователь выбрал "Да", перейти на вкладку корзины
                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new BusketPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
