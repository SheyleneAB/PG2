using StripClientWPFStripView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace StripClientWPFStripView.Services
{
    public class StripService
    {
        private HttpClient client;
        public StripService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7051/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task<Strip> GetStripAsync(string path)
        {
            try
            {
                Strip v = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    v = await response.Content.ReadAsAsync<Strip>();

                }
                return v;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return null; }
        }
    }
}
