using RentalOfPremises.Application.DTOs;
using System.Net;

namespace RentalOfPremises.API.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        //private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next/*, ILogger<ExceptionHandlingMiddleware> logger*/)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound, "Запись не найдена!");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError, "Что-то не так!");
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context, 
            string exMsg, 
            HttpStatusCode statusCode, 
            string message)
        {
            //_logger.LogError(exMsg);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            var errorDto = new ErrorDto
            {
                Message = message,
                StatusCode = (int)statusCode
            };

            string result = errorDto.ToString();
            await response.WriteAsync(result);
        }
    }
}
