using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlineMarketPlaceTorico.Server
{
    public partial class Product
    {
        public int Id { get; set; }
        public int? SellerId { get; set; }
        public int? CategoryId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public string Brand { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Quantity { get; set; }
    }
}
