using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HttpClientBug
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DoRequest();
            Console.Read();
        }

        public async static void DoRequest()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
                                         | DecompressionMethods.Deflate
                                         | DecompressionMethods.Brotli
            };
            
            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 1234");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");

            string address = "https://www.nespresso.com/ru/ru/rest/V1/alice/customers/getProfile";
            Console.WriteLine($"Request to {address}");

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync(
                        address,
                        new StringContent("{}", Encoding.UTF8, "application/json")
                    ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error, it doesn't work");
                Console.WriteLine(e);
                return;
            }

            string stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Console.WriteLine($"OK got response, it works:\n{stringResponse}");
        }
    }
}
