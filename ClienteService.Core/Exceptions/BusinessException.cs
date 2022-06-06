using System;
using System.Collections.Generic;

namespace ClienteService.Core.Exceptions
{
    public class BusinessException : Exception
    {
        #region VARIABLES
        public IDictionary<string, string[]> Errors;
        #endregion

        #region CONSTRUCTOR
        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(IDictionary<string, string[]> Errors)
        {
            this.Errors = Errors;
        }
        #endregion
    }
}
