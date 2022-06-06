using ClienteService.Core.Entities.Api;
using ClienteService.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;

namespace ClienteService.Infrastructure.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        #region METHODS
        public void OnException(ExceptionContext context)
        {
            int _status = 0;
            string _header = string.Empty;
            Exception _exception = context.Exception;
            IDictionary<string, string[]> _errors = null;

            switch (context.Exception.GetType())
            {
                case Type T when T == typeof(BusinessException):
                    _status = (int)HttpStatusCode.BadRequest;
                    _header = "Logic Exception";
                    _errors = (_exception as BusinessException).Errors ?? ExceptionToDictionary(_exception);
                    break;
                default:
                    _status = (int)HttpStatusCode.InternalServerError;
                    _header = "Server Error";
                    _errors = ExceptionToDictionary(_exception);
                    break;
            }

            var validation = new ApiError()
            {
                status = _status,
                title = _header,
                errors = _errors
            };

            context.Result = new ObjectResult(validation);
            context.HttpContext.Response.StatusCode = _status;
            context.ExceptionHandled = true;
        }

        private IDictionary<string, string[]> ExceptionToDictionary(Exception exception)
        {
            var dictionary = new Dictionary<string, string[]>();
            dictionary.Add("detail", new string[] { exception.Message });
            return dictionary;
        }
        #endregion
    }
}
