using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketPlaceEnd.Models
{
    public partial class User
    {
        public string FullName
        {
            get
            {
                return $"{FName} {Name}";
            }


        }

        public Visibility BtnVisible
        {
            get
            {
                if (Account.AuthUser.RoleId == 1)
                {
                    return Visibility.Visible;

                }
                else
                { return Visibility.Collapsed; }

            }
        }

    }
}
