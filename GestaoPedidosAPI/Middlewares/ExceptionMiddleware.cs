using GestaoPedidos.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace GestaoPedidosAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Erro interno da aplicação.");
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string mensagem)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                success = false,
                message = mensagem
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
