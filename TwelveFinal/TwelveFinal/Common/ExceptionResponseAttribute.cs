using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TwelveFinal
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;

            var result = exception.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }


    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Message) : base(Message)
        {
        }
    }

    public class BadRequestException : Exception
    {
        public DataDTO DataDTO { get; }
        public object DataDTOs { get; }
        private static string ModifyMessage(object DataDTO)
        {
            return JsonConvert.SerializeObject(DataDTO);
        }

        public BadRequestException(DataDTO DataDTO) : base(ModifyMessage(DataDTO))
        {
            this.DataDTO = DataDTO;
        }

        public BadRequestException(object DataDTOs) : base(ModifyMessage(DataDTOs))
        {
            this.DataDTOs = DataDTOs;
        }

        public BadRequestException(string Message) : base(Message) { }

    }

    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string Message) : base(Message) { }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException(string Message) : base(Message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        //private static string ModifyMessage()
        //{
        //    return JsonConvert.SerializeObject(message);
        //}
        public NotFoundException(string Message) : base(Message)
        {
        }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string Message) : base(Message)
        {
        }
    }
}