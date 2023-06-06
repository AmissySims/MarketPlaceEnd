using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketPlaceEnd.Models
{
    public partial class Provider
    {
        public Visibility BtnVisible
        {
            get
            {
                if (Account.AuthUser.RoleId == 2)
                { return Visibility.Collapsed; }
                else { return Visibility.Visible; }
            }
        }
    }
}
