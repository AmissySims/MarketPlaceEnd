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
                if (StatusOrderId == 7 || StatusOrderId == 6 || Account.AuthUser.RoleId == 4 || Account.AuthUser.RoleId == 5 || Account.AuthUser.RoleId == 3)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility BtnVisible1
        {
            get
            {
                if (StatusOrderId == 5 && (Account.AuthUser.RoleId == 4 || Account.AuthUser.RoleId == 5))
                {
                    return Visibility.Visible;

                }
                else { return Visibility.Collapsed; }

            }
        }
    }
}
