using MarketPlaceEnd.Models;
using MarketPlaceEnd.Windows;
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

namespace MarketPlaceEnd.Pages.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        public List<Product> bucketList;
        public List<Product> Bucket { get; set; }
       
        //public Order Order { get; set; }
        public AddOrderPage(List<Product> bucketList)
        {
           
            InitializeComponent();
            Bucket = bucketList;
            var points = App.db.DeliveryPoint.ToList();
            DeliveryPointCb.ItemsSource = points;
            var types = App.db.DeliveryType.ToList();
            DateTb.Text = DateTime.Now.ToString();
            NameTb.Text = Account.AuthUser.FullName;
            var cards = App.db.Cards.Where(x => x.UserId == Account.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;
            PriceTb.Text = $"{Convert.ToString(Bucket.Sum(b => b.Count * b.Price))} руб.";





        }

        private void DeliveryCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DeliveryPointCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Pickup_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
        }

        private void Pickup_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Hidden;
        }

        private void Courier_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Hidden;
        }

        private void Courier_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            Order ord = new Order();
            {
                ord.UserId = Account.AuthUser.Id;
                ord.AdressDelivery = AdressTb.Text;
                ord.StatusOrderId = 1;
                ord.DeliveryPointId = (DeliveryPointCb.SelectedItem as DeliveryPoint).Id;
                if (Courier.IsChecked == true)
                {
                    ord.DeliveryTypeId = 2;
                }
                else if(Pickup.IsChecked == true)
                {
                    ord.DeliveryTypeId = 1;
                }
                else
                {
                    MessageBox.Show("Выберите тип доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
                    return;
                }
                    
                ord.Date = DateTime.Now;
                ord.Price =  Bucket.Sum(b => b.Count * b.Price) ;


            }
            //Добавление в Order
            App.db.Order.Add(ord);

            //Удаление корзины
            var bucketsToRemove = App.db.Bucket.Where(b => b.UserId == Account.AuthUser.Id).ToList();
            App.db.Bucket.RemoveRange(bucketsToRemove);

            //Добавление в OrderProduct
            foreach (var b in bucketsToRemove)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = b.ProductId,
                    Count = b.Quantity,
                    OrderId = ord.Id

                };
                App.db.OrderProduct.Add(orderProduct);
                
      
            }

            //Сохранение
            var selCard = (CardCb.SelectedItem as Cards);
            selCard.Balance -= Bucket.Sum(b => b.Count * b.Price);
            App.db.SaveChanges();
            MessageBox.Show("Заказ успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new ProductsPage());
        }

        private void CardCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddCardBt_Click(object sender, RoutedEventArgs e)
        {
            AddCardWindow card = new AddCardWindow((sender as Button).DataContext as Cards);
            card.ShowDialog();
            var cards = App.db.Cards.Where(x => x.UserId == Account.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;

        }
    }
}
