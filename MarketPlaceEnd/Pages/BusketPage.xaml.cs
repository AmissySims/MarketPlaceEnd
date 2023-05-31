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

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для BusketPage.xaml
    /// </summary>
    public partial class BusketPage : Page
    {
        private decimal TotalPrice;
        public static List<Product> bucketList { get; set; }
        public BusketPage()
        {
            InitializeComponent();
            //Отображение корзины конкретного пользователя
            bucketList = new List<Product>();
            bucketList = App.db.Bucket
                    .Where(b => b.ProductId == b.Product.Id || b.UserId == Account.AuthUser.Id)
                    .Select(b => b.Product) 
                    .ToList();

            foreach (var item in bucketList)
            {
                decimal totalPrice = (decimal)(item.Price * item.Count);

                TotalPrice = totalPrice;
            }
            LIstBucket.ItemsSource = bucketList;
        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderPage(bucketList));
        }

        private void DeleteCommand(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
