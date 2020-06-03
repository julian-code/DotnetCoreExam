using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    public static class ConsumerApiClient
    {
        static public async Task<HttpResponseMessage> ValidateDateTimeUsingProviderApi(string baseUri)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(baseUri)})
            {
                try
                {
                    var response = await client.GetAsync($"/api/provider");
                    return response;
                }
                catch (System.Exception ex)
                {
                    throw new Exception("There was a problem connecting to Provider API.", ex);
                }
            }
        }
    }
}