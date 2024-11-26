using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitnessDomein.Model;

namespace ConsoleAppFitnessHTTPClient
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:5035");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var v = await GetMemberAsync("api/member/2");
        }

        static async Task GetMemberAsync(string v)
        {
            try
            {
                Member visitor = null;
                HttpRequestMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    visitor = await response.Content.ReadAsAsync<Member>();
                }
                return visitor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); return null;
            }

        }
    }
}
