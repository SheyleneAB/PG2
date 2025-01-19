using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WPFAppFitnessHTTPClient.Model;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPFAppFitnessHTTPClient.Services
{
    class FitnessService
    {
        private HttpClient client;
        public FitnessService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7067/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<bool> SchrijfReservatieAsync(string path, ReservationDTO reservatie)
        {
            try
            {
                // Serialize using System.Text.Json
                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(reservatie), // System.Text.Json.JsonSerializer.Serialize
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response = await client.PostAsync(path, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<List<Equipment>> GeefLijstEquipment(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Equipment>>();
            }
            return new List<Equipment>();
        }
        public async Task<List<TimeSlot>> GetAvailableTimeSlots (string path, int equipmentId, DateTime date)
        {
        
            try
            {
                string fullPath = $"{path}/{equipmentId}/{date:yyyy-MM-dd}";

                HttpResponseMessage response = await client.GetAsync(fullPath);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<TimeSlot>>();
                }
                else
                {
                    return new List<TimeSlot>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;  
            }
        }
        
       
    }
}
