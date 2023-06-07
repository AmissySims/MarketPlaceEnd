using MarketPlaceEnd.Models;
using MarketPlaceEnd.Pages.AddEditPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для BusketPage.xaml
    /// </summary>
    public partial class BusketPage : Page
    {
        public static List<BucketItem> bucketList { get; set; }
        public BusketPage()
        {
            InitializeComponent();
            //Отображение корзины конкретного пользователя
           
            bucketList = App.db.Bucket
                    .Where(b => b.ProductId == b.Product.Id && b.UserId == Account.AuthUser.Id)
                    .Select(b => new BucketItem
                    {
                        Product = b.Product,
                        Quantity = b.Quantity
                    })
                    .ToList();

            LIstBucket.ItemsSource = bucketList;

        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            foreach (var buc in bucketList)
            {
                int countProd = App.db.Product.Where(p => p.Id == buc.Product.Id).Select(p => p.Count).FirstOrDefault() ?? -1;
                if (countProd < buc.Product.Count)
                {
                    MessageBox.Show($"Остаток на складе {countProd}, укажите верное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (countProd == -1)
                {
                    MessageBox.Show("Ошибка товара на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            NavigationService.Navigate(new AddOrderPage(bucketList));
        }

        private void DeleteCommand(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (sender as Button).DataContext as BucketItem;
            if (selectedProduct != null)
            {
                try
                {
                    var rm = App.db.Bucket.Where(b => b.ProductId == selectedProduct.Product.Id).FirstOrDefault();
                    App.db.Bucket.Remove(rm);
                    App.db.SaveChanges();
                    bucketList.Remove(selectedProduct);
                    LIstBucket.ItemsSource = null;
                    LIstBucket.ItemsSource = bucketList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить товар из корзины. Ошибка: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            TextBlock totalPriceTextBlock = FindTotalPriceTextBlock(currentTextBox);
            if (totalPriceTextBlock.DataContext is BucketItem product)
            {
                if (Int32.TryParse(currentTextBox.Text, out Int32 currentCount))
                {
                    var productInBucket = App.db.Bucket.FirstOrDefault(b => b.ProductId == product.Product.Id);
                    if (productInBucket != null)
                    {
                        productInBucket.Quantity = currentCount;
                        App.db.SaveChanges();
                    }
                    else
                    {
                        return;
                    }

                    totalPriceTextBlock.Text = (product.Product.Price * currentCount).ToString();
                }
                else
                {
                    totalPriceTextBlock.Text = 0.ToString();
                }
            }
        }

        private TextBlock FindTotalPriceTextBlock(TextBox currentTextBox)
        {
            FrameworkElement parent = currentTextBox;
            while (parent != null)
            {
                if (parent.FindName("TotalPriceTb") is TextBlock totalPriceTextBlock)
                {
                    return totalPriceTextBlock;
                }
                parent = parent.Parent as FrameworkElement;
            }
            return null;
        }

        private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T element && element.Name == name)
                {
                    return element;
                }
                else
                {
                    T foundElement = FindVisualChild<T>(child, name);
                    if (foundElement != null)
                        return foundElement;
                }
            }
            return null;
        }

        private void LIstBucket_Loaded(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            foreach (var item in listView.Items)
            {
                ListViewItem listViewItem = (ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(item);
                if (listViewItem != null)
                {
                    TextBox countTextBox = FindVisualChild<TextBox>(listViewItem, "CountTb");
                    TextBlock totalPriceTextBlock = FindVisualChild<TextBlock>(listViewItem, "TotalPriceTb");

                    if (countTextBox != null && totalPriceTextBlock != null)
                    {
                        if (countTextBox.DataContext is BucketItem product)
                        {
                            if (int.TryParse(countTextBox.Text, out int currentCount))
                            {
                                totalPriceTextBlock.Text = (product.Product.Price * currentCount).ToString();
                            }
                            else
                            {
                                totalPriceTextBlock.Text = "0";
                            }
                        }
                    }
                }
            }
        }

        private void CountTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if(!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
