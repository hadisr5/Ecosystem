using System;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Messenger
{
    public class EmailMessenger : IMessageSender
    {
        public async Task<SendMsgResult> SendMessage(string Message, string Receiver)
        {
            try
            {
                //TODO : send email

                return new SendMsgResult() { Sent = true, Message = "" };
            }
            catch (Exception ex)
            {
                return new SendMsgResult
                {
                    Sent = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<SendMsgResult> Verify(string Message, string Receiver, string template)
        {
            return await Verify(Message, Receiver);
        }
        public async Task<SendMsgResult> Verify(string Message, string Receiver)
        {
            try
            {
                //TODO : send verification email

                return new SendMsgResult() { Sent = true, Message = "" };
            }
            catch (Exception ex)
            {
                return new SendMsgResult
                {
                    Sent = false,
                    Message = ex.Message
                };
            }
        }
    }
}
