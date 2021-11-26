
namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;

    public class MessagesViewModel:CoreBaseViewModel
    {
        public int SenderUserID { get; set; }
        public int ReceiverUserID { get; set; }
        public string MsgTitle { get; set; }
        public string MsgType { get; set; }
        public int MsgViewed { get; set; }

    }
}
