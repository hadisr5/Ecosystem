using Microsoft.Extensions.Options;
using Seventy.DomainClass;
using System;
using System.Threading.Tasks;

namespace Seventy.Service.Core.Messenger
{
    public class SMSMessenger : IMessageSender
    {
        private readonly IOptions<PublicConfiguration> _AppSetting;

        public string lineNumber { get; set; } = "10008566";
        public string APICode { get; set; } = "f266f3783928826581abfcca2a7e6379d62624c23340f7b0ba9c5121e1aeb3c7";

        public SMSMessenger()
        {

        }

        public SMSMessenger(IOptions<PublicConfiguration> options)
        {
            _AppSetting = options;

            var smsConf = _AppSetting.Value.smsConfig.Split(';');
            lineNumber = smsConf[0];
            APICode = smsConf[1];
        }

        public async Task<SendMsgResult> SendMessage(string Message, string Receiver)
        {
            try
            {
                if (string.IsNullOrEmpty(Receiver))
                    return new SendMsgResult
                    {
                        Sent = false
                        ,
                        Message = "null Receiver"
                    };

                Receiver = Receiver.Trim();

                var sms = new Ghasedak.Core.Api(APICode);
                var result = await sms.SendSMSAsync(Message, Receiver, lineNumber);
                return new SendMsgResult() { Sent = true, Message = result.Result.Message };
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
        public async Task<SendMsgResult> Verify(string Message, string Receiver)
        {
            return await Verify(Message, Receiver, "UserActivationCode");
        }
        public async Task<SendMsgResult> Verify(string Message, string Receiver, string template)
        {
            var sendMsgResult = new SendMsgResult();
            try
            {
                if (string.IsNullOrEmpty(Receiver))
                {
                    sendMsgResult.Message = "null Receiver";
                    return sendMsgResult;
                }

                Receiver = Receiver.Trim();

                var sms = new Ghasedak.Core.Api(APICode);
                var result = await sms.VerifyAsync(1, template, new string[] { Receiver }, Message);
                sendMsgResult.Sent = true;
                sendMsgResult.Message = result?.Result?.Message;
                return sendMsgResult;
            }
            catch (Exception ex)
            {
                sendMsgResult.Message =ex.Message;
                return sendMsgResult;
            }
        }
    }
}
