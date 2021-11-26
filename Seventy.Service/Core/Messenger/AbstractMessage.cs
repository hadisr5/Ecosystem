using System.Threading.Tasks;

namespace Seventy.Service.Core.Messenger
{
    public abstract class AbstractMessage
    {
        protected IMessageSender messageSender;
        public abstract Task<SendMsgResult> SendMessage(string Message, string Receiver);
        public abstract Task<SendMsgResult> Verify(string Message, string Receiver);
        public abstract Task<SendMsgResult> Verify(string Message, string Receiver, string template);
    }
}
