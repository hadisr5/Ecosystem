using System.ComponentModel;

namespace Seventy.ViewModel.Core.UserGroupMember
{
    public class UserGroupMembersViewModel : CoreBaseViewModel
    {
        public int UserID { get; set; }
        public int UserGroupID { get; set; }

        [DisplayName("نام کاربر")]
        public string UserName { get; set; }

        [DisplayName("نام گروه کاربر")]
        public string UserGroupName { get; set; }
    }
}
