using BulletinBoard.Domain.Constants;
using BulletinBoard.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BulletinBoard.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                AdNotFoundException => (HttpStatusCode.NotFound, ErrorMessages.TitleNotFound),
                InvalidAdException => (HttpStatusCode.BadRequest, ErrorMessages.TitleValidationError),

                _ => (HttpStatusCode.InternalServerError, ErrorMessages.TitleServerError)
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, "CRITICAL API ERROR: {Message}", exception.Message);
            }
            else
            {
                _logger.LogWarning("Business Validation Triggered: {Message}", exception.Message);
            }

            httpContext.Response.StatusCode = (int)statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = statusCode == HttpStatusCode.InternalServerError
                    ? "An unexpected error occurred on the server. Please try again later."
                    : exception.Message,
                Instance = httpContext.Request.Path
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; 
        }
    }
}


