using OnlineMarketPlaceTorico.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlaceTorico.Shared
{
    public class ItemsRepository
    {
        static public string View { get; set; } = "List";
        static public int CurrentSellerId = -1;
        static public List<Product> ItemsList { get; set; } = null;
        static public List<Category> Categories { get; set; } = null;
        static public List<string> Pictures { get; set; } = new List<string> { "pant.jpg", "shirt.jpg", "shoes.jpg","shoes2.jpg","shoes3.jpg","shoes4.jpg","watch.jpg","watch2.jpg","watch3.jpg","watch4.jpg"};
    }
}
