﻿using System;
using System.Net;
using System.Threading.Tasks;
using HelixAssignment.ErrorHandlers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HelixAssignment.ErrorHandlers
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var customException = exception as BaseCustomException;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = String.Format("Unexpected error - {0}", exception.Message);
            var description = String.Format("Unexpected error - {0}", exception.StackTrace.ToString());

            if (null != customException)
            {
                message = customException.Message;
                description = customException.Description;
                statusCode = customException.Code;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorResponse
            {
                Message = message,
                Description = description,
                IsSuccessStatusCode = false,
                StatusCode = statusCode
            }));
        }
    }


    public class CustomErrorResponse
    {
        public string Message { get; set; }
        public string Description { get; set; }

        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }

    }

    public class NotFoundCustomException : BaseCustomException
    {
        public NotFoundCustomException(string message, string description) : base(message, description, (int)HttpStatusCode.NotFound)
        {
        }
    }

}
