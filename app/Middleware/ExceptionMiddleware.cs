using System;
using System.Net;
using Project.Models;
using Project.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace Project.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == 401) {                	
                    throw new UnauthorizedResourceException("");
                }
            } catch (UnauthorizedResourceException e) {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, UnauthorizedResourceException exception)
        {
            return context.Response.WriteAsync(new ErrorDetails(){
                StatusCode = context.Response.StatusCode,
                Message = "Você não tem autorização para acessar este recurso!"
            }.ToString());
        }
    }
}