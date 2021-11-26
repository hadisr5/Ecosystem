using System;
using Seventy.Common.Enums;

namespace Seventy.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
             : base(eApiResultStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(eApiResultStatusCode.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(eApiResultStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(eApiResultStatusCode.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(eApiResultStatusCode.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(eApiResultStatusCode.NotFound, message, exception, additionalData)
        {
        }
    }
}
