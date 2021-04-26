using AddressBook.Common.Mvc.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AddressBook.Common.Mvc
{
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _request;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _request = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => InvokeAsync(context);

        private async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                var httpStatusCode = ConfigurateExceptionTypes(exception);

                var response = new { code = httpStatusCode, message = exception.Message };
                var payload = JsonConvert.SerializeObject(response);
                context.Response.ContentType = JsonContentType;
                context.Response.StatusCode = httpStatusCode;

                await context.Response.WriteAsync(payload);
            }
        }

        /// <summary>
        /// Configurates/maps exception to the proper HTTP error Type
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode;

            // Exception type To Http Status configuration
            switch (exception)
            {
                case var _ when exception is NotFoundException:
                    httpStatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case var _ when exception is BadRequestException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return httpStatusCode;
        }
    }
}
