using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
