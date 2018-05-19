using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Models;
using Newtonsoft.Json;

namespace CarPriceCalculator
{
    public class CarPriceCalculator
    {
        const int numVehicles = 500;
        const string vehicleUrl = "http://localhost:11111/api/values/";
        const string discountUrl = "http://localhost:22222/api/values/";
        const string taxUrl = "http://localhost:33333/api/values/";
        const string savePriceUrl = "http://localhost:55555/api/values/";

        private static async Task<Vehicle> GetVehicleAsync(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(vehicleUrl);
            string result;
            Vehicle vehicle = null;
            HttpResponseMessage response = await client.GetAsync(string.Empty + id);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                vehicle = JsonConvert.DeserializeObject<Vehicle>(result);
            }
            else
            {
                Console.WriteLine("Get vehicle details failed for ID " + id + " with error " + response.ReasonPhrase);
            }
            return vehicle;
        }

        private static async Task<Discount> GetDiscountAsync(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(discountUrl);
            string result;
            Discount discount = null;
            HttpResponseMessage response = await client.GetAsync(id + string.Empty);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                discount = JsonConvert.DeserializeObject<Discount>(result);
            }
            else
            {
                Console.WriteLine("Get discount failed for ID " + id + " with error " + response.ReasonPhrase);
            }
            return discount;
        }

        private static async Task<TaxResult> GetTaxAsync(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(taxUrl);
            string result;
            TaxResult tax = null;
            HttpResponseMessage response = await client.GetAsync(string.Empty + id);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                tax = JsonConvert.DeserializeObject<TaxResult>(result);
            }
            else
            {
                Console.WriteLine("Get tax failed for ID " + id + " with error " + response.ReasonPhrase);
            }
            return tax;
        }

        private static async Task UpdateSalePrice(int id, SalePrice salePrice)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(savePriceUrl);
            var content = new StringContent(JsonConvert.SerializeObject(salePrice), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(string.Empty + id, content);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Update sale price failed for ID " + id + " with error " + response.ReasonPhrase);
        }

        private const int delay = 100;

        public static async Task Process()
        {
            try
            {
                var vehicleMap = new Dictionary<int, Task<Vehicle>>(numVehicles);
                var taxMap = new Dictionary<int, Task<TaxResult>>(numVehicles);
                var discountMap = new Dictionary<int, Task<Discount>>(numVehicles);
                var saveMap = new Dictionary<int, Task>(numVehicles);

                for (int i = 1; i <= numVehicles; i++)
                {
                    vehicleMap[i] = GetVehicleAsync(i);
                    //System.Threading.Thread.Sleep(delay);
                    discountMap[i] = GetDiscountAsync(i);
                    //System.Threading.Thread.Sleep(delay);
                    taxMap[i] = GetTaxAsync(i);
                    //System.Threading.Thread.Sleep(delay);
                }

                for (int i = 1; i <= numVehicles; i++)
                {
                    var vehicle = await vehicleMap[i];
                    var discount = await discountMap[i];

                    if (null != vehicle && null != discount)
                    {
                        var price = vehicle.msrp;
                        price = price - price * discount.DiscountPercent / 100;

                        var tax = await taxMap[i];

                        if (tax != null)
                        {
                            price = price + price * tax.TaxPercent / 100;
                            price += tax.Fees;

                            saveMap[i] = UpdateSalePrice(i, new SalePrice { Price = price });
                            //System.Threading.Thread.Sleep(delay);
                        }
                    }
                }

                for (int i = 1; i <= numVehicles; i++)
                {
                    if (saveMap.ContainsKey(i))
                        await saveMap[i];
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void Main(string[] args)
        {
            Process().Wait();
        }
    }
}
