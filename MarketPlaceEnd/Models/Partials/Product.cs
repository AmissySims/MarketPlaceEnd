using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MarketPlaceEnd.Models
{
    public partial class Product
    {
        public byte[] MainPhoto
        {
            get
            {
                var firstPhoto = this.ProductPhoto.FirstOrDefault();
                if (firstPhoto != null)
                {
                    return firstPhoto.Image;
                }
                return null;
            }
        }
    
        public Visibility BtnVisible 
        { 
            get
            {
                if(Account.AuthUser.RoleId == 1)
                {
                    return Visibility.Visible;
                    
                }
                else
                { return Visibility.Collapsed; }
               
            }
        }
        public string Availability
        {
            get
            {
                if (Count > 0)
                {
                    return $"В наличии";
                }
                else { return $"Нет в наличии"; }
            }
        }
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
        public Visibility BtnVisible2
        {
            get
            {
                if (Account.AuthUser.RoleId == 4 || Account.AuthUser.RoleId == 5)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
      
        public SolidColorBrush ColorCount
        {
            get
            {

                if (Count > 0)
                    return Brushes.Green;
                else
                    return Brushes.Red;
            }

        }

        public string ColorCount1
        {
            get
            {

                if (Count > 0)
                    return "White";
                else
                    return "BurlyWood";
            }

        }
    }

    public class BucketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
