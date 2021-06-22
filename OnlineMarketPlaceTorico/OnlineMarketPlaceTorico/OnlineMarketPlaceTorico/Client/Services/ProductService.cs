using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnlineMarketPlaceTorico.Server;
using OnlineMarketPlaceTorico.Shared;

namespace OnlineMarketPlaceTorico.Client.Services
{
    public class ProductService
    {
        private readonly HttpClient httpClient;
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Product>> GetLatestProducts()
        {
            return await this.httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44390/api/Online/firstTen");
        }
        public async Task<Seller?> getShopDetails(int? sellerId)
        {
            return await this.httpClient.GetFromJsonAsync<Seller?>("https://localhost:44390/api/Online/getShopDetails/" + sellerId);
        }

        public async Task<List<Product>> GetNavSearchedProducts(string navbarString,string?category)
        {
            return await this.httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44390/api/Online/relatedItems?searchString="+navbarString+"&category="+category);
        }

        public async Task<List<Product>?> GetFlexibleSearchedProducts(string SearchFilter,bool SearchByBrandName,bool SearchByProductName,bool SearchByShopName,int minNumber,int maxNumber,bool AscendOrder)  //when this function will be called nothing will be null parameter
        {
            return await this.httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44390/api/Online/filter?searchString="+SearchFilter+"&brandName="+SearchByBrandName+"&productName="+SearchByProductName+"&shopName="+SearchByShopName+"&minPrice="+minNumber+"&maxPrice="+maxNumber+"&ascendOrder="+AscendOrder);
        }
        public async Task<List<Product>> GetShopProducts(int sellerId)
        {
            return await this.httpClient.GetFromJsonAsync<List<Product>>("https://localhost:44390/api/online/getShopProducts/" + sellerId) ;
        }
        public async Task<List<Seller>> GetRelatedShops(string searchString)
        {
            return await this.httpClient.GetFromJsonAsync<List<Seller>>("https://localhost:44390/api/online/getShops/" + searchString);
        }
        public async Task<int> GetSellerId(string sellerEmail)
        {
            return await this.httpClient.GetFromJsonAsync<int>($"https://localhost:44390/api/online/getSellerId/{sellerEmail}");
        }
        public async Task<Seller> GetSeller(string sellerEmail)
        {
            return await this.httpClient.GetFromJsonAsync<Seller>($"https://localhost:44390/api/online/getSeller/{sellerEmail}");
        }
        public async Task UpdateSeller(int sellerId, Seller updatedSeller)
        {
            await this.httpClient.PutAsJsonAsync($"https://localhost:44390/api/online/updateSeller/{sellerId}",updatedSeller);
        }
        public async Task<List<Product>> GetProductsListOfSeller(int sellerId)
        {
            return await this.httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:44390/api/online/getShopProducts/{sellerId}");
        }
        public async Task<List<Category>> GetCategoriesList()
        {
            return await this.httpClient.GetFromJsonAsync<List<Category>>($"https://localhost:44390/api/online/getAll");
        }
        public async Task<List<Seller>> GetAllShops()
        {
            return await this.httpClient.GetFromJsonAsync<List<Seller>>("https://localhost:44390/api/online/getAllShops");
        }
    }
}