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

        }
    }
}
