using System.ComponentModel;
using Seventy.ViewModel;

namespace Seventy.DomainClass.Core
{
    public class UserDocumentsEditModel : CoreBaseViewModel
    {
        [DisplayName("کاربر")]
        public int UserID { get; set; }

        [DisplayName("نوع")]
        public int DocumentTypeID { get; set; }

        [DisplayName("فایل")]
        public int FileID { get; set; }
    }
}
