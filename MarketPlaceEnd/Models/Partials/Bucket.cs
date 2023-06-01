using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MarketPlaceEnd.Models
{
    public partial class Bucket
    {
        public decimal? TotalCost
        {
            get
            {
                return Quantity * Product.Price;
            }
        }
        //public decimal? TotalPricee
        //{
        //    get
        //    {
        //        return this.Product.Sum(x => x.Quantity * x.Product.Price);
        //    }
        //}
    }
}
