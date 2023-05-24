using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
