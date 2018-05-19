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
        const string taxUrl = "http://localhost/33333/api/values/";
        const string savePriceUrl = "http://localhost/55555/api/values/";

        private static HttpClient client = new HttpClient();

        private static async Task<Vehicle> GetVehicleAsync(int id)
        {
            string result;
            Vehicle vehicle = null;
            HttpResponseMessage response = await client.GetAsync(vehicleUrl + id);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                vehicle = JsonConvert.DeserializeObject<Vehicle>(result);
            }
            else
            {
                throw new ApplicationException("Get vehicle details failed for ID " + id);
            }
            return vehicle;
        }

        private static async Task<Discount> GetDiscountAsync(int id)
        {
            string result;
            Discount discount = null;
            HttpResponseMessage response = await client.GetAsync(discountUrl + id);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                discount = JsonConvert.DeserializeObject<Discount>(result);
            }
            else
            {
                throw new ApplicationException("Get vehicle details failed for ID " + id);
            }
            return discount;
        }

        private static async Task<TaxResult> GetTaxAsync(int id)
        {
            string result;
            TaxResult tax = null;
            HttpResponseMessage response = await client.GetAsync(taxUrl + id);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                tax = JsonConvert.DeserializeObject<TaxResult>(result);
            }
            else
            {
                throw new ApplicationException("Get vehicle details failed for ID " + id);
            }
            return tax;
        }

        private static async Task UpdateSalePrice(int id, SalePrice salePrice)
        {
            var content = new StringContent(JsonConvert.SerializeObject(salePrice));
            var response = await client.PutAsync(savePriceUrl + id, content);
        }

        public static async Task Process()
        {
            var vehicleMap = new Dictionary<int, Task<Vehicle>>(numVehicles);
            var taxMap = new Dictionary<int, Task<TaxResult>>(numVehicles);
            var discountMap = new Dictionary<int, Task<Discount>>(numVehicles);
            var saveMap = new Dictionary<int, Task>(numVehicles);

            for (int i = 0; i <= numVehicles; i++)
            {
                vehicleMap[i] = GetVehicleAsync(i);
                discountMap[i] = GetDiscountAsync(i);
                taxMap[i] = GetTaxAsync(i);
            }

            for (int i = 0; i <= numVehicles; i++)
            {
                var vehicle = await vehicleMap[i];
                var discount = await discountMap[i];

                var price = vehicle.msrp;
                price = price - price * discount.DiscountPercent / 100;

                var tax = await taxMap[i];

                price = price + price * tax.TaxPercent / 100;
                price += tax.Fees;

                saveMap[i] = UpdateSalePrice(i, new SalePrice { Price = price });
            }

            for (int i = 0; i <= numVehicles; i++)
            {
                await saveMap[i];
            }
        }

        public static void Main(string[] args)
        {
        }
    }
}
