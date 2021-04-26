namespace AddressBook.Business.Configuration.Models
{
    public class HttpRetryPolicySettings
    {
        public int RetryCount { get; set; }

        public int RetryStartTime { get; set; }
    }
}
