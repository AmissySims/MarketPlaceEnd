using MarketPlaceEnd.Models;
using MarketPlaceEnd.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MarketPlaceEnd.Pages.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        public List<BucketItem> Bucket { get; set; }

        public AddOrderPage(List<BucketItem> bucketList)
        {
            InitializeComponent();
            Bucket = bucketList;
            var points = App.db.DeliveryPoint.ToList();
            DeliveryPointCb.ItemsSource = points;
            var types = App.db.DeliveryType.ToList();
            DateTb.Text = DateTime.Now.ToString();
            NameTb.Text = Account.AuthUser.FullName;
            Adress.Visibility = Visibility.Hidden;
            IfPickup.Visibility = Visibility.Hidden;
            var cards = App.db.Cards.Where(x => x.UserId == Account.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;

            PriceTb.Text = $"{Convert.ToString(Bucket.Sum(b => b.Quantity * b.Product.Price))} руб.";
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
            Adress.Visibility = Visibility.Hidden;
        }

        private void Pickup_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
            Adress.Visibility = Visibility.Visible;
        }

        private void Courier_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Hidden;
            Adress.Visibility = Visibility.Visible;
        }

        private void Courier_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.db.Bucket.ToList().Count <= 0)
                {
                    MessageBox.Show("Ваша корзина пуста", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var selCard = (CardCb.SelectedItem as Cards);
                if (selCard.Balance < Bucket.Sum(b => b.Quantity * b.Product.Price))
                {
                    MessageBox.Show("На карте недостаточно средств! Выберите другую карту", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Order ord = new Order();
                {
                    ord.UserId = Account.AuthUser.Id;

                    ord.StatusOrderId = 1;

                    if (Courier.IsChecked == true)
                    {
                        ord.DeliveryTypeId = 2;
                        ord.DeliveryPointId = null;
                        ord.AdressDelivery = AdressTb.Text;
                    }
                    else if (Pickup.IsChecked == true)
                    {
                        ord.DeliveryTypeId = 1;
                        ord.DeliveryPointId = (DeliveryPointCb.SelectedItem as DeliveryPoint).Id;
                        ord.AdressDelivery = null;

                    }
                    else
                    {
                        MessageBox.Show("Выберите тип доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ord.Date = DateTime.Now;
                    ord.Price = Bucket.Sum(b => b.Quantity * b.Product.Price);
                }
                //Добавление в Order
                App.db.Order.Add(ord);

                //Удаление корзины
                var bucketsToRemove = App.db.Bucket.Where(b => b.UserId == Account.AuthUser.Id).ToList();
                App.db.Bucket.RemoveRange(bucketsToRemove);

                //Добавление в OrderProduct
                foreach (var b in Bucket)
                {
                    var orderProduct = new OrderProduct
                    {
                        ProductId = b.Product.Id,
                        Count = b.Quantity,
                        OrderId = ord.Id
                    };

                    //Минус товар на складе
                    BucketItem selectedProd = App.db.Product
                            .Where(p => p.Id == orderProduct.ProductId)
                            .Select(p => new BucketItem
                            {
                                Product = p,
                                Quantity = (int)p.Count
                            })
                            .FirstOrDefault();
                    selectedProd.Product.Count -= b.Quantity;
                    App.db.OrderProduct.Add(orderProduct);

                }

                //Сохранение

                selCard.Balance -= Bucket.Sum(b => b.Quantity * b.Product.Price);
                App.db.SaveChanges();
                MessageBox.Show("Заказ успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ProductsPage());



            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при оформлении заказа: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
