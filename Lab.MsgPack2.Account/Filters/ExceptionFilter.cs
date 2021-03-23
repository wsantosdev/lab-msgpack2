using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Lab.MsgPack2.Account.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException || context.Exception is InvalidOperationException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                return;
            }

            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
