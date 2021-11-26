using System.ComponentModel;

namespace Seventy.ViewModel.Core.UserGroupMember
{
    public class UserGroupMembersEditModel : CoreBaseViewModel
    {
        [DisplayName("کاربر")]
        public int UserID { get; set; }

        [DisplayName("گروه کاربری")]
        public int UserGroupID { get; set; }
    }
}
