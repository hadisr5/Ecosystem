using System.ComponentModel;

namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;

    public class UserDocumentsViewModel : CoreBaseViewModel
    {
        public int UserID { get; set; }
        public int DocumentTypeID { get; set; }

        [DisplayName("فایل")]
        public int FileID { get; set; }

        [DisplayName("کاربر")]
        public string UserName { get; set; }

        [DisplayName("نوع")]
        public string DocumentTypeTitle { get; set; }
    }
}
