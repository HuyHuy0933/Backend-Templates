using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.API.Common.Exceptions
{
	public class GlobalExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionMiddleware> _logger;
		public GlobalExceptionMiddleware(RequestDelegate next,
			ILogger<GlobalExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				await HandleExceptionAsync(httpContext, ex);
			}
		}
		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			HttpStatusCode status;
			string message;
			var stackTrace = String.Empty;

			var exceptionType = exception.GetType();
			switch (exceptionType)
			{
				//todo
				default:
					status = HttpStatusCode.InternalServerError;
					message = "Unknown error occurs";
					break;
			}


			var result = JsonSerializer.Serialize(message);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)status;
			return context.Response.WriteAsync(result);
		}
	}
}
