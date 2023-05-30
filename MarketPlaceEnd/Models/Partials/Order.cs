using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketPlaceEnd.Models
{
    public partial class Order
    {
        public int? Quanity
        {
            get
            {
                return this.OrderProduct.Sum(x => x.Count);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.OrderProduct.Sum(x => x.Count * x.Product.Price);
            }
        }
        public Visibility BtnVisible
        {
            get
            {
                if (StatusOrderId == 7)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
    }
}
