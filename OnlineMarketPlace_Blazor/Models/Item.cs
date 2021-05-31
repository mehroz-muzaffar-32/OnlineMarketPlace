using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace_Blazor.Models
{
    public  class Item
    {
        public string ItemName { get; set; }
        public string Brand { get; set; } = "None";
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Category { get; set; } = "Other";
        public int Quantity { get; set; } = 0;
    }
}
