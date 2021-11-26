using System;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Messenger
{
    public class ShortMessage : AbstractMessage
    {
        public ShortMessage(IMessageSender messageSender)
        {
            //Initialize the super class messageSender variable
            this.messageSender = messageSender;
        }
        public async override Task<SendMsgResult> SendMessage(string Message, string Receiver)
        {
            if (Message.Length <= 10)
            {
                return await messageSender.SendMessage(Message, Receiver);
            }
            else
            {
                Console.WriteLine("Unable to send the message as length > 10 characters");
                return null;
            }
        }

        public async override Task<SendMsgResult> Verify(string Message, string Receiver)
        {
            if (Message.Length <= 10)
            {
                return await messageSender.Verify(Message, Receiver);
            }
            else
            {
                Console.WriteLine("Unable to send the message as length > 10 characters");
                return null;
            }
        }
        public async override Task<SendMsgResult> Verify(string Message, string Receiver,string template)
        {
            if (Message.Length <= 10)
            {
                return await messageSender.Verify(Message, Receiver,template);
            }
            else
            {
                Console.WriteLine("Unable to send the message as length > 10 characters");
                return null;
            }
        }

    }
}
