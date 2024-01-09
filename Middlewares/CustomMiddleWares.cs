using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameItems.Middlewares
{
	public class CustomMiddleWares
	{
		private readonly RequestDelegate _next;

		public CustomMiddleWares(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch(Exception ex) 
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Error"));
				
			}
		}
	}
	public static class CustomMiddleWaresExetension
	{
		public static IApplicationBuilder UseCustomExeptionMiddle(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomMiddleWares>();
		}
	}
}
