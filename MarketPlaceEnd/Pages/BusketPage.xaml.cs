﻿using MarketPlaceEnd.Models;
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

            LIstBucket.ItemsSource = bucketList;
        }

        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            foreach (var buc in bucketList)
            {
                int countProd = App.db.Product.Where(p => p.Id == buc.Id).Select(p => p.Count).First() ?? -1;
                if (countProd < buc.Count)
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
            var selectedProduct = (sender as Button).DataContext as Product;
            if (selectedProduct != null)
            {
                try
                {
                    var rm = App.db.Bucket.Where(b => b.ProductId == selectedProduct.Id).FirstOrDefault();
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
            if (totalPriceTextBlock.DataContext is Product product)
            {
                if (Int32.TryParse(currentTextBox.Text, out Int32 currentCount))
                {
                    var productInBucket = App.db.Bucket.FirstOrDefault(b => b.ProductId == product.Id);
                    if (productInBucket != null)
                    {
                        productInBucket.Quantity = currentCount;
                        App.db.SaveChanges();
                    }
                    else
                    {
                        return;
                    }
                    product.Count = currentCount;
                    totalPriceTextBlock.Text = (product.Price * currentCount).ToString();
                } else
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
            return;
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
                        if (countTextBox.DataContext is Product product)
                        {
                            if (int.TryParse(countTextBox.Text, out int currentCount))
                            {
                                product.Count = currentCount;
                                totalPriceTextBlock.Text = (product.Price * currentCount).ToString();
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
    }
}
