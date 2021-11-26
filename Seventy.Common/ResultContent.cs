using Newtonsoft.Json;
using System.Reflection;

namespace Seventy.Common.Api
{
    public class ResultContent
    {
        public ResultContent(bool isSuccess, string message = null) : base()
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class ResultContent<TData> : ResultContent where TData : class
    {
        public ResultContent(bool isSuccess, TData data, string message = null) : base(isSuccess, message)
        {
            Data = data;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }
    }
}
