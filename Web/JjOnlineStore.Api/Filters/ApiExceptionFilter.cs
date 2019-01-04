using System.Net;
using JjOnlineStore.Common.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JjOnlineStore.Api.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ApiExceptionFilter(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            const int status = (int)HttpStatusCode.InternalServerError;

            var result = _hostingEnvironment.IsDevelopment() ?
                new JsonResult(context.Exception) :
                new JsonResult(new Error("An unexpected internal server error has occurred."));

            context.HttpContext.Response.StatusCode = status;
            context.Result = result;
        }
    }
}