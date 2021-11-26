
using Seventy.Data;
using System.Threading.Tasks;

namespace Seventy.Service.SMS
{
    public interface ISMSService
    {
        Task<Ghasedak.Core.Models.Results.SendResult> SendSMSAsync(string message, string receptor);
    }
}
