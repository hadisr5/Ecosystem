
namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;

    public class TicketsViewModel:CoreBaseViewModel
    {
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Section { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Actions { get; set; }
        public int ResponderUserID { get; set; }
        public string Response { get; set; }
    }
}
