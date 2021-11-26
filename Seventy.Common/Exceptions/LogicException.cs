using System;
using Seventy.Common.Enums;

namespace Seventy.Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException()
           : base(eApiResultStatusCode.LogicError)
        {
        }

        public LogicException(string message)
            : base(eApiResultStatusCode.LogicError, message)
        {
        }

        public LogicException(object additionalData)
            : base(eApiResultStatusCode.LogicError, additionalData)
        {
        }

        public LogicException(string message, object additionalData)
            : base(eApiResultStatusCode.LogicError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(eApiResultStatusCode.LogicError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(eApiResultStatusCode.LogicError, message, exception, additionalData)
        {
        }
    }
}
