using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineMarketPlaceTorico.Shared;

namespace OnlineMarketPlaceTorico.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : ControllerBase
    {
        private OnlineMarketPlace_DBContext database;
        public OnlineController()
        {
            database = new OnlineMarketPlace_DBContext();
        }
        [HttpGet]
        public List<Category> GetAll()
        {
            return database.Category.ToList(); 
        }

        [HttpGet]
        [Route("firstTen")]
        public List<Product> FirstTenLatest() //will deliver first 10 latest items
        {
            return database.Product.OrderByDescending(p => p.CreationDate).Take(5).ToList();
        }
        [HttpGet]
        [Route("relatedItems/{searchString}")]
        public List<Product> RelatedProducts(String? searchString) //will search product name aur brand name
        {
            return database.Product.Where(p=>p.Name.ToUpper()==searchString.ToUpper()||p.Brand.ToUpper()==searchString.ToUpper()).ToList();
        }

        

        [HttpGet]
        [Route("findCat")]
        public string? findCategory(int? id=null) //tested
        {
            OnlineMarketPlace_DBContext db = new OnlineMarketPlace_DBContext();
            Category?cat= db.Category.Where(p => p.Id == id).FirstOrDefault();
            return (cat!=null ? cat.Name : null);
        }

        [HttpGet]
        [Route("findShopName")]
        public string? findShopName(int? sellerId=null) //tested
        {
            OnlineMarketPlace_DBContext db = new OnlineMarketPlace_DBContext();
            Seller? seller = db.Seller.Where(s => s.Id == sellerId).FirstOrDefault();
            return (seller != null ? seller.ShopName : null);
        }

        //Mehroz
        [HttpGet]
        [Route("getShopDetails/{sellerId}")]     //id is compulsory
        public Seller? GetShopDetails(int SellerId)
        {
            return database.Seller.Where(s => s.Id == SellerId).FirstOrDefault();
        }

        //Mehroz
        [HttpGet]
        [Route("getShopProducts/{sellerId}")]   //id is compulsory
        public List<Product> GetProductsListOfSeller(int sellerId)
        {
            return database.Product.Where( p => p.SellerId == sellerId).ToList();
        }

        //Mehroz
        [HttpGet]
        [Route("getShopProducts/{sellerId}/{category}")]  //sellerId and category is compulsory
        public List<Product> GetProductsListOfSeller(int sellerId, string? category = null) //before Enumerable, query work at backend, after that query will be work at 
        {
            return  database.Product.Where(p => p.SellerId == sellerId).AsEnumerable().Where(p => isSameCategory(category,p.CategoryId)).ToList();
        }

        //This is just a helper function...
        public bool isSameCategory(string?category,int? categoryId)
        {
            if (category == null)
                return false;
            return findCategory(categoryId).ToUpper() == category.ToUpper();
        }


        //Mehroz
        //Tested
        [HttpPost]
        [Route("add")]
        public bool AddProduct(Product product)   //product is neccessary otherwise this method will not trigger
        {
            try
            {
                database.Product.Add(product);
                database.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }


        //Mehroz
        [HttpPut]
        [Route("update/{productId}")]    //product is neccessary otherwise this method will not trigger
        public bool UpdateProduct(Product updatedProduct,int productId)
        {
            Product? pro = database.Product.Where(p => p.Id == productId).FirstOrDefault();
            if (pro == null)
                return false;
            if (updatedProduct.SellerId != null)
                pro.SellerId = updatedProduct.SellerId;
            if (updatedProduct.CategoryId != null)
                pro.CategoryId = updatedProduct.CategoryId;
            if (updatedProduct.Picture != null)
                pro.Picture = updatedProduct.Picture;
            if (updatedProduct.Name != null)
                pro.Name = updatedProduct.Name;
            if (updatedProduct.Price!=null)
                pro.Price = updatedProduct.Price;
            if (updatedProduct.Discount != null)
                pro.Discount = updatedProduct.Discount;
            if (updatedProduct.Brand != null)
                pro.Brand = updatedProduct.Brand;
            if (updatedProduct.CreationDate != null)
                pro.CreationDate = updatedProduct.CreationDate;
            if (updatedProduct.Quantity != null)
                pro.Quantity = updatedProduct.Quantity;

            database.SaveChanges();
            return true;
        }
        [HttpDelete]
        [Route("delete/{productId}")]
        public bool RemoveProduct(int ProductId)   //Id is compulsory otherwise, this method will not trigger
        {
            try
            {
                Product? pro = database.Product.Where(p => p.Id == ProductId).FirstOrDefault();
                if (pro != null)
                {
                    database.Product.Remove(pro);
                    database.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}