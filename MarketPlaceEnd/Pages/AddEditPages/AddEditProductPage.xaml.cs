using MarketPlaceEnd.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product contextProduct;
        DbPropertyValues oldValues;
        public AddEditProductPage(Product product)
        {
            InitializeComponent();
            CbType.ItemsSource = App.db.TypeProduct.ToList();
            CbProv.ItemsSource = App.db.Provider.ToList();
            contextProduct = product;
            DataContext = contextProduct;
            if (contextProduct.Id != 0)
            {
                oldValues = App.db.Entry(contextProduct).CurrentValues.Clone();
            }
            Refresh();
         

        }

        private void ImageBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog() { Multiselect = true };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    if (dialog.FileNames.Length > 0)
                    {
                        foreach (var item in dialog.FileNames)
                        {
                            contextProduct.ProductPhoto.Add(new ProductPhoto()
                            {
                                Image = File.ReadAllBytes(item),
                                Product = contextProduct
                            });
                        }

                        Refresh();
                        DataContext = null;
                        DataContext = contextProduct;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении картинки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Refresh()
        {
            LVPhoto.ItemsSource = contextProduct.ProductPhoto.ToList();
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contextProduct.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextProduct.Description))
                {
                    MessageBox.Show("Заполните поле описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextProduct.Price == null)
                {
                    MessageBox.Show("Заполните поле стоимости", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }

                if (contextProduct.Provider == null)
                {
                    MessageBox.Show("Выберите поставщика", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
                if (contextProduct.Count == null)
                {
                    MessageBox.Show("Заполните поле количества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
                if (contextProduct.TypeProduct == null)
                {
                    MessageBox.Show("Выберите тип продукта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextProduct.Id == 0)
                    {
                        App.db.Product.Add(contextProduct);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
           
        }


        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {

            if (oldValues != null)
            {
                App.db.Entry(contextProduct).CurrentValues.SetValues(oldValues);

            }
            NavigationService.GoBack();
        }

        private void PriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void DeleteImageBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sel = LVPhoto.SelectedItem as ProductPhoto;
                if (sel != null)
                {
                    if (MessageBox.Show("Точно хотите удалить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        App.db.ProductPhoto.Remove(sel);
                        App.db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Выберите изображение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Refresh();
                }
            }
           catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении картинки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
