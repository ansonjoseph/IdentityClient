using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;
namespace WenMax.Security.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            // discover endpoints from the metadata by calling Auth server hosted on 5000 port
            var discoveryClient = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discoveryClient.IsError)
            {
                Console.WriteLine(discoveryClient.Error);
                return;
            }

            // request the token from the Auth server
            var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "ajoseph", "password123");
            var response = await tokenClient.RequestClientCredentialsAsync("api1");

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);

        }

    }
}
