using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Contacts.Application.Middleware;

public class GlobalExceptionsHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
		try
		{
			await next(context);
		}
		catch (ValidationException ex)
        {
            await ModifyContext(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
		{
            await ModifyContext(context, StatusCodes.Status500InternalServerError, ex.Message);
		}
    }

    private static async Task ModifyContext(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Detail = message,
            Status = statusCode,
        }), CancellationToken.None);
    }
}
