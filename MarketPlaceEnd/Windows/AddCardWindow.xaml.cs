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
using System.Windows.Shapes;
using MarketPlaceEnd.Models;

namespace MarketPlaceEnd.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window
    {
        public Cards card { get; set; }
        public AddCardWindow(Cards cards)
        {
            InitializeComponent();
            card = cards;
            BalanceTb.Text = "0";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }

        private void NumberCard_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void DateTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && (e.Text != "/"))
            {
                e.Handled = true;
            }
        }

        private void CVCTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BalanceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void AddCardBt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NumberCardTb.Text))
            {
                MessageBox.Show("Заполните поле номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(DateTb.Text))
            {
                MessageBox.Show("Заполните поле даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(CVCTb.Text))
            {
                MessageBox.Show("Заполните поле CVC", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                Cards card = new Cards();
                {
                    card.NumberCard = NumberCardTb.Text;
                    card.DateCard = DateTb.Text;
                    card.UserId = Account.AuthUser.Id;
                    card.CVC = CVCTb.Text;
                    card.Balance = Convert.ToDecimal(BalanceTb.Text);
                }

                App.db.Cards.Add(card);
                App.db.SaveChanges();
                MessageBox.Show("Карта добавлена", "Уведомлние", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
        }
    }
}
