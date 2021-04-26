using Microsoft.AspNetCore.Builder;

namespace AddressBook.Common.Mvc
{
    public static class Extensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
         => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
