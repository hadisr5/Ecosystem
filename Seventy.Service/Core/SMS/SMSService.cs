using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Seventy.Data;
using Seventy.Repository.Core;
using Seventy.Service.Core.Logs;
using System.Threading.Tasks;


namespace Seventy.Service.SMS
{
    public class SMSService :  ISMSService
    {
        public async Task<Ghasedak.Core.Models.Results.SendResult> SendSMSAsync(string message, string receptor)
        {
            try
            {
                if (string.IsNullOrEmpty(receptor))
                    return new Ghasedak.Core.Models.Results.SendResult()
                    {
                        Result = new Ghasedak.Core.Models.Results.ResultItems()
                        { Code = 404, Message = "null receptor" }
                    };

                receptor = receptor.Trim();

                string lineNumber = "10008566";
                var sms = new Ghasedak.Core.Api("50cc0b6e34d9a1fd16e2b36c8e035025a063ce474a915b1fd61d684c80e8b1f1");
                var result = await sms.SendSMSAsync(message, receptor, lineNumber);
                return result;
            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                return new Ghasedak.Core.Models.Results.SendResult()
                {
                    Result = new Ghasedak.Core.Models.Results.ResultItems()
                    { Code = 404, Message = ex.Message }
                };
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                return new Ghasedak.Core.Models.Results.SendResult()
                {
                    Result = new Ghasedak.Core.Models.Results.ResultItems()
                    { Code = 404, Message = ex.Message }
                };
            }
        }
    }
}
