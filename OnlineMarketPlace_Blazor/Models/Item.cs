using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace_Blazor.Models
{
    public  class Item
    {
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
