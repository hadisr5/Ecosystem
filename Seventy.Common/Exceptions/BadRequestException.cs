using System;
using Seventy.Common.Enums;

namespace Seventy.Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(eApiResultStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(eApiResultStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(eApiResultStatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(eApiResultStatusCode.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(eApiResultStatusCode.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(eApiResultStatusCode.BadRequest, message, exception, additionalData)
        {
        }
    }
}
