using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineMarketPlace.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("Index");
        }
        public ViewResult SellerProfileView()
        {
            return View();
        }

        public ViewResult BuyerView()
        {
            return View();
        }
    }
}
