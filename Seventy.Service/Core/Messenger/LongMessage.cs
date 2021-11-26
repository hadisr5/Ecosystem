using System.Threading.Tasks;

namespace Seventy.Service.Core.Messenger
{
    public class LongMessage : AbstractMessage
    {
        public LongMessage(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }
        public async override Task<SendMsgResult> SendMessage(string Message, string Receiver)
        {
            return await messageSender.SendMessage(Message, Receiver);
        }

        public async override Task<SendMsgResult> Verify(string Message, string Receiver)
        {
            return await messageSender.Verify(Message, Receiver);
        }
        public async override Task<SendMsgResult> Verify(string Message, string Receiver,string template)
        {
            return await messageSender.Verify(Message, Receiver,template);
        }
    }
}
