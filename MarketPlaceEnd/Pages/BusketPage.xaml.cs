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
        public static List<Product> productList { get; set; }
        public BusketPage()
        {
            InitializeComponent();
            //LIstBucket.ItemsSource = App.db.OrderProduct.ToList();
            productList = new List<Product>();

        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderPage(new Order()));
        }
    }
}
