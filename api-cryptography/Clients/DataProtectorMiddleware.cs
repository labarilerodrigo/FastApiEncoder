using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FastApiEncoder.Clients
{
    public class DataProtectorMiddleware {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public DataProtectorMiddleware(RequestDelegate next, IConfiguration configuration) {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context) {
            // Validate the Media Type. 
            //   Valid => application/json
            if (!context.Request.Headers.TryGetValue(DataProtectorConstants.ContentTypeHeaderName, out var extractedContentTypeValue)) {
                context.Response.StatusCode = 415;
                await context.Response.WriteAsync("Content-Type definition missing");
                return;
            }
            var ContentTypeValue = DataProtectorConstants.ContentTypeHeaderValue;
            if (!ContentTypeValue.Equals(extractedContentTypeValue)) {
                context.Response.StatusCode = 415;
                await context.Response.WriteAsync("Unsupported Media Type");
                return;
            }

            // Validate the payload
            var length = context.Request.ContentLength;
            if (length ==0) {
                context.Response.StatusCode = 406;
                await context.Response.WriteAsync("Not Acceptable. Invalid payload");
                return;
            }

            await _next(context);
        }

    }
}
