using System.ComponentModel;

namespace Seventy.ViewModel.Features.Tickets
{
    public class TicketListViewModel : CoreBaseViewModel
    {
        [DisplayName("عنوان")]
        public string Title { get; set; }
        [DisplayName("بخش")]
        public string Section { get; set; }
        [DisplayName("اولویت")]
        public string Priority { get; set; }
        [DisplayName("وضعیت")]
        public string Status { get; set; }
        [DisplayName("عملیات")]
        public string Actions { get; set; }
        [DisplayName("پاسخ")]
        public string Response { get; set; }
    }
}
