using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EIA.Domain.Model;
using PoliCommon.WebApiClient;

namespace EIA.Services.Clients
{
    public class EiaClient : WebApiClientBase
    {
        private readonly string _subscriptionKey;

        public EiaClient(HttpClient client) : base(client)
        {
            _subscriptionKey = Client.DefaultRequestHeaders.Where(x => x.Key == "Subscription-Key").First().Value.First();
        }

        public async Task<Category> GetCategoryByIdAsync(string categoryId)
        {
            string path = "category/".WithQueryString("api_key", _subscriptionKey).WithQueryString("category_id", categoryId);
            return await this.GetAsync<Category>(path, "category");
        }

        public async Task GetSeriesByIdAsync(string seriesId)
        {
            string path = "series/".WithQueryString("api_key", _subscriptionKey).WithQueryString("series_id", seriesId);
            HttpResponseMessage message = await Client.GetAsync(path);
        }
    }
}
