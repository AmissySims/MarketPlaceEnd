using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    public class BucketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}
