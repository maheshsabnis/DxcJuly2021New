using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientNS;
namespace CS_API_Client
{
    class Program
    {
        static async Task  Main(string[] args)
        {
            Console.WriteLine("Press Any Key when API Starts");
            Console.ReadLine();

            HttpClient client = new HttpClient();
            //var response = await client.GetAsync("http://localhost:64061/api/SimpleAPI");
            //Console.WriteLine(await response.Content.ReadAsStringAsync());

            var apiClient = new ClientProxy("http://localhost:64061/", client);
            var res = await apiClient.GetallAsync();
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(res));


            Console.ReadLine();
        }
    }
}
