using System.Collections.Generic;

namespace AddressBook.Common.Models
{
    public class PostgresOptions
    {
        public bool Enabled { get; set; }

        public Dictionary<string, string> Connections { get; set; } = new Dictionary<string, string>();

        public bool EnableSensitiveDataLogging { get; set; } = true;

        public bool EnableDetailedErrors { get; set; } = true;

        public bool UseQueryTrackingBehavior { get; set; } = true;

        public bool EnableRetryOnFailure { get; set; } = true;

        public string MigrationHistoryTableName { get; set; }

        public string MigrationHistoryTableSchema { get; set; }

        public bool EnableServiceProviderCaching { get; set; } = true;
    }
}
