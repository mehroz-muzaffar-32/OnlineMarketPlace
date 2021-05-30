using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace_Blazor.Models
{
    public class ItemsRepository
    {
        static public string View { get; set; } = "List";
        static public List<Item> ItemsList = new List<Item>
        {
            new Item{ItemName="Shaper Shoes",Brand="Shaper",Image="/shoes.jpg",Price=1234,Discount=12},
            new Item{ItemName="Nike Shoes",Image="/shoes2.jpg",Price=12000,Discount=30},
            new Item{ItemName="Ladies Shoes",Image="/shoes3.jpg",Price=40000,Discount=20},
            new Item{ItemName="Baby Shoes",Image="/shoes4.jpg",Price=50000,Discount=10},
            new Item{ItemName="Red Berry",Image="/shirt.jpg",Price=40000,Discount=5},
            new Item{ItemName="Brown Digger",Image="/watch.jpg",Price=900000,Discount=15},
            new Item{ItemName="Office Love",Image="/watch2.jpg",Price=35000,Discount=25},
            new Item{ItemName="Gold Mine",Image="/watch3.jpg",Price=400000,Discount=30},
            new Item{ItemName="Royal",Image="/watch4.jpg",Price=5000000,Discount=35},
            new Item{ItemName="Legs Fitter",Image="/pant.jpg",Price=4000,Discount=50}
        };
    }
}
