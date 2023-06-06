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
    /// Логика взаимодействия для DeliveryPountsPage.xaml
    /// </summary>
    public partial class DeliveryPountsPage : Page
    {
        public DeliveryPountsPage()
        {
            InitializeComponent();
          
        }

        private void EditPoint_Click(object sender, RoutedEventArgs e)
        {
            var selPoint = (sender as Button).DataContext as DeliveryPoint;
            NavigationService.Navigate(new AddDeliveryPointPage(selPoint));
        }
        private void Refresh()
        {
            PointsList.ItemsSource = App.db.DeliveryPoint.ToList();

        }
       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddPointBt_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new AddDeliveryPointPage(new DeliveryPoint()));
        }
    }
}
