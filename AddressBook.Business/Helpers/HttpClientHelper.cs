using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;

namespace AddressBook.Business.Helpers
{
    public static class HttpClientHelper
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount, int retryStartTime)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryStartTime, retryAttempt)));
        }
    }
}
