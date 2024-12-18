using System;
using System.Net;
using System.Text.Json;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace timer
{
    public class Function1
    {
        private readonly KeyClient _keyClient;

        public Function1()
        {
            // connectie met vault
            string keyVaultUrl = "https://mykeyfrom.vault.azure.net/";
            _keyClient = new KeyClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        }

        [Function("KeyVaultGetKeys")]
        public async Task Run([TimerTrigger("0 30 9 * * *")] TimerInfo timer, FunctionContext context)
        {
            var logger = context.GetLogger("KeyVaultGetKeys");

            logger.LogInformation($"Function executed at: {DateTime.UtcNow}");

            try
            {
                List<string> expiringKeyDetails = new List<string>();
                DateTime now = DateTime.UtcNow;
                // Retrieve the threshold value from app settings
                string thresholdValue = Environment.GetEnvironmentVariable("ThresholdMonths");

                // Fallback to a default value if not set or invalid
                int tresholdint = 13; // Default value
                if (!string.IsNullOrWhiteSpace(thresholdValue) && int.TryParse(thresholdValue, out int parsedThreshold))
                {
                    tresholdint = parsedThreshold;
                }
                DateTime threshold = now.AddMonths(tresholdint);

                // Verkrijgen van keys uit de keyvault
                await foreach (KeyProperties key in _keyClient.GetPropertiesOfKeysAsync())
                {
                    // kijk als de key een experatie datum heeft + onder de treshold zit
                    if (key.ExpiresOn.HasValue && key.ExpiresOn.Value <= threshold && key.ExpiresOn.Value > now)
                    {
                        string expiry = key.ExpiresOn.Value.ToString("yyyy-MM-dd");
                        expiringKeyDetails.Add($"Name: {key.Name}, Expiry: {expiry}");
                    }
                }

                // loggen van de keys die aan de condities voldoen
                if (expiringKeyDetails.Count > 0)
                {
                    logger.LogInformation($"Expiring keys within the next {tresholdint} months: " + JsonSerializer.Serialize(expiringKeyDetails));
                }
                else
                {
                    logger.LogInformation("No keys are expiring within the next 3 months.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching keys: {ex.Message}");
            }
        }
    }
}
