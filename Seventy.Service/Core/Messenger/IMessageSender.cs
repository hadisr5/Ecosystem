using System.Threading.Tasks;

namespace Seventy.Service.Core.Messenger
{
    public interface IMessageSender
    {
        Task<SendMsgResult> SendMessage(string Message, string Receiver);
        Task<SendMsgResult> Verify(string Message, string Receiver);
        Task<SendMsgResult> Verify(string Message, string Receiver, string template);
    }
}
