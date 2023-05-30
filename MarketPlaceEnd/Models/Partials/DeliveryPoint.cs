using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketPlaceEnd.Models
{
    public partial class DeliveryPoint
    {
        public Visibility BtnVisible1
        {
            get
            {
                if (Account.AuthUser.RoleId == 2 || Account.AuthUser.RoleId == 1)
                {
                    return Visibility.Visible;

                }
                else
                { return Visibility.Collapsed; }

            }
        }
    }
}
