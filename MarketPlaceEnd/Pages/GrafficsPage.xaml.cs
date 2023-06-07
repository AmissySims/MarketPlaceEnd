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
    /// Логика взаимодействия для GrafficsPage.xaml
    /// </summary>
    public partial class GrafficsPage : Page
    {
        public GrafficsPage()
        {
            InitializeComponent();
            var area = MainChart.ChartAreas.Add("MainArea");
            var legend = MainChart.Legends.Add("Main legend");
            area.AxisX.Interval = 1;
            area.AxisY.Interval = 1;
        }

        private void GenerateBt_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDate.SelectedDate;
            var endDate = EndDate.SelectedDate;
            if (!startDate.HasValue)
            {
                MessageBox.Show("Выберите даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainChart.Series.Clear();
            //var order in App.db.Order
            var seria = MainChart.Series.Add("Количество заказов");
            var chartDate = App.db.Order.ToList().Where(z => z.Date >= startDate.Value.Date && z.Date <= endDate).OrderBy(u => u.UserId)
                .GroupBy(x => x.User.FullName)
                .ToDictionary(key => key.Key, value => value.Count());
            seria.Points.DataBindXY(chartDate.Keys, chartDate.Values);
            seria.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //foreach (var order in App.db.Order)
            //{
            //    var seria = MainChart.Series.Add($"#{order.Id} {order.User.FullName}");
            //    var chartDate = App.db.Order.ToList().Where(z => z.Date >= startDate.Value.Date && z.Date <= endDate).OrderBy(u => u.UserId)
            //        .GroupBy(x => x.User.FullName)
            //        .ToDictionary(key => key.Key, value => value.Count());
            //    seria.Points.DataBindXY(chartDate.Keys, chartDate.Values);
            //    seria.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column ;

            //}
        }
    }
}
