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
        [Route("getAll")]
        public List<Category> GetAll()
        {
            return database.Category.ToList(); 
        }

        //Used in homepage
        [HttpGet]
        [Route("firstTen")]
        public List<Product> FirstTenLatest() //will deliver first 10 latest items
        {
            return database.Product.OrderByDescending(p => p.CreationDate).Take(10).ToList();
        }
        //Used in searchBar
        [HttpGet]
        [Route("relatedItems")] //string is necassry to send in request, its a part o route
        public List<Product> RelatedProducts(string searchString, string? category=null) //will search product name aur brand name
        {
            return database.Product.AsEnumerable().Where(p=>p.Name.ToUpper()==searchString.ToUpper()||p.Brand.ToUpper()==searchString.ToUpper() || findCategory(p.CategoryId)?.ToUpper() == category?.ToUpper()).ToList();
        }


        /*                      This function is not according to need
        [HttpGet]
        [Route("filter")]
        //this is a main function which will perform filtering
        //It is returning 2 lists, filtering will be based on searchString(shop or products) if shops then we will return back both, list
        //of shops and their related products
        //This is independent of parameters, there could be all parameters in url and some or nothing, wether
        //all unassigned parameters will be set to null, which means it is not included in filtering
        public Tuple<List<Product>?, List<Seller>?> FilteredProducts(String? category = null, String? searchString = null, String? selectedType = null, bool brandName = false, bool shopName = false, bool productName = false, int? minPrice = null, int? maxPrice = null, bool? isAscending = null)
        {
            //IQueryable query;
            List<Product>? products = null;
            List<Seller>? sellers = null;
            if (selectedType == "shop")              // in == null comparion will not be considered, while in "likes", null comparison will be considered
            {
                //query = database.Seller.AsQueryable();
                //A Good link to learn: https://stackoverflow.com/questions/44681362/an-expression-tree-lambda-may-not-contain-a-null-propagating-operator
                sellers = database.Seller.AsEnumerable().Where
                    (
                    p =>
                    p.Email?.ToUpper() == searchString?.ToUpper() ||
                    p.Contact?.ToUpper() == searchString?.ToUpper() ||
                    p.Address?.ToUpper() == searchString?.ToUpper() ||
                    (shopName==true ? p.ShopName?.ToUpper() == searchString?.ToUpper() : false)
                    ).ToList();

                if (sellers != null) //if sellers list is not null
                {
                    products = new List<Product>();  //I have to take union of products, which means union with previous record, if it is previously null, it could arise an issue
                    foreach (var seller in sellers)     //collect productsa of each seller
                        products.AddRange(database.Product.Where(p => p.SellerId == seller.Id).ToList());
                    //GoodLink To Learn: https://stackoverflow.com/questions/13505672/simplest-way-to-form-a-union-of-two-lists
                }
            }
            else
            {
                products = database.Product.AsEnumerable().    //by using AsEnumerable all products will be fetched to client end, so now we can use functions inside query
                    Where
                    (
                    p =>
                    findCategory(p.CategoryId)?.ToUpper() == category?.ToUpper() ||    //if selected category rom dropdown is matched..
                    (brandName==true? p.Brand?.ToUpper() == searchString?.ToUpper():false) ||                  
                    (productName==true? p.Name?.ToUpper() == searchString?.ToUpper():false) ||
                    (shopName==true? findShopName(p.SellerId)?.ToUpper() == searchString?.ToUpper():false) ||
                    (p.Price >= minPrice && p.Price <= maxPrice)
                    ).ToList();
            }
            return (new Tuple<List<Product>?, List<Seller>?>(products, sellers));
        }*/

        [HttpGet]
        [Route("filter")]
        public List<Product> FilteredProducts(string searchString, bool brandName, bool productName, bool shopName, int minPrice, int maxPrice, bool ascendOrder)
        {
            var query=database.Product.AsEnumerable().
                Where(p => (brandName==true ? p.Brand.ToUpper() == searchString.ToUpper() : false) || (productName==true ? p.Name.ToUpper() == searchString.ToUpper() : false) || (shopName==true ? findShopName(p.SellerId)?.ToUpper()==searchString.ToUpper() : false) || checkPriceRange((int)p.Price,minPrice,maxPrice));
            if (ascendOrder==true)
                query=query.OrderBy(p => p.CreationDate);
            else
                query=query?.OrderByDescending(p => p.CreationDate);
            return query.ToList();
        }

        bool checkPriceRange(int productPrice,int minRange, int maxRange)
        {
            if (minRange != 0 && maxRange != 0)
                return productPrice >= minRange && productPrice <= maxRange;
            if (minRange != 0)
                return productPrice >= minRange;
            if (maxRange != 0)
                return productPrice <= maxRange;
            return false;
        }


        [HttpGet]
        [Route("getShops/{searchString}")]
        public List<Seller> GetShops(string searchString)
        {
            List<Seller> s = database.Seller.Where(s => s.ShopName.ToUpper() == searchString.ToUpper() || s.Address.ToUpper() == searchString.ToUpper() || s.Contact.ToUpper() == searchString.ToUpper() || s.Email.ToUpper() == searchString.ToUpper()).ToList();
            return s;
        }



        /*[HttpGet]
        [Route("filterName")]
        public Tuple<string?, string?, string?, string?, string?, string?> fun(String? category = null, String? searchString = null, String? selectedType = null, String? brandName = null, String? shopName = null, String? productName = null)
        {
            return new Tuple<string, string?, string?, string?, string?, string?>(category, searchString, selectedType, brandName, shopName, productName);
        }*/



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
            return findCategory(categoryId)?.ToUpper() == category?.ToUpper();
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
        [HttpPost]
        [Route("addNewSeller/{newSellerJSON}")]
        public bool AddNewSeller(string newSellerJSON)
        {
            try
            {
                Seller newSeller = System.Text.Json.JsonSerializer.Deserialize<Seller>(newSellerJSON);
                database.Seller.Add(newSeller);
                database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpGet]
        [Route("getSeller/{sellerEmail}")]
        public Seller GetSeller(string sellerEmail)
        {
            return database.Seller.Where(s => s.Email == sellerEmail).FirstOrDefault();
        }
        [HttpGet]
        [Route("getSellerId/{sellerEmail}")]
        public int GetSellerId(string sellerEmail)
        {
            return database.Seller.Where(s => s.Email == sellerEmail).FirstOrDefault().Id;
        }
        [HttpDelete]
        [Route("removeSeller/{sellerEmail}")]
        public bool RemoveSeller(string sellerEmail)
        {
            Seller? sel = database.Seller.Where(s => s.Email == sellerEmail).FirstOrDefault();
            if (sel != null)
            {
                database.Seller.Remove(sel);
                database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        [Route("getCategoryName/{categoryId}")]   //id is compulsory
        public string GetCategoryName(int categoryId)
        {
            return database.Category.Where(c=>c.Id==categoryId).FirstOrDefault().Name;
        }
    }
}