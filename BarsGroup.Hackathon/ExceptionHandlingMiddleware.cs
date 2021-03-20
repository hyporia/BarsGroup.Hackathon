using BarsGroup.Hackathon.Core.Exceptions;
using BarsGroup.Hackathon.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
// ReSharper disable ConvertToConstant.Local
// ReSharper disable SuggestBaseTypeForParameter

namespace BarsGroup.Hackathon
{
	/// <summary>
	/// Обработчик ошибок API
	/// </summary>
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ApplicationExceptionBase ex)
			{
				await HandleApplicationExceptionBaseAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task WriteResponseAsync(HttpContext context,
			string errorText,
			HttpStatusCode responseCode)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)responseCode;

			var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
			await context.Response.WriteAsync(JsonSerializer.Serialize(new BaseResponse
			{
				Error = errorText
			},
			jsonOptions.Value.JsonSerializerOptions));
		}

		private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
		{
			var errorText = exception.Message;
			var responseCode = HttpStatusCode.NotFound;
			await WriteResponseAsync(context, errorText, responseCode);
		}

		private async Task HandleApplicationExceptionBaseAsync(HttpContext context, ApplicationExceptionBase exception)
		{
			var errorText = exception.Message;
			var responseCode = HttpStatusCode.BadRequest;
			await WriteResponseAsync(context, errorText, responseCode);
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var errorText = $"{exception.Message}";
			var responseCode = HttpStatusCode.InternalServerError;
			await WriteResponseAsync(context, errorText, responseCode);
		}
	}
}
