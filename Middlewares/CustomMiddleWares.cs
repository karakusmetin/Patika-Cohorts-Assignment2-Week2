using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
				string message = "[Request]  HTTP" + context.Request.Method + " - " + context.Request.Path;
				Console.WriteLine(message);

				await _next.Invoke(context);

				message = "[Response]  HTTP" + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode ;
				Console.WriteLine(message);
			}
			catch(Exception ex) 
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				string message = "[Error]  HTTP" + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message;
				Console.WriteLine(message);
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
