using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace AddressBook.Common.Helpers
{
    public class EnvironmentHelper
    {
        public static string GetCurrentEnvironmentVariable(string environmentKey = "ASPNETCORE_ENVIRONMENT")
        {
            var environment = Environment.GetEnvironmentVariable(environmentKey);

            var environments = new[]
            {
                Environments.Development,
                Environments.Staging,
                Environments.Production
            };

            if (environment == null || !environments.Contains(environment, StringComparer.OrdinalIgnoreCase))
            {
                Environment.SetEnvironmentVariable(environmentKey, Environments.Development);
            }

            return Environment.GetEnvironmentVariable(environmentKey);
        }
    }
}
