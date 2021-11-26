using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core.Users
{
    public class UserGroupMemberViewModel : CoreBaseViewModel
    {
        public int UserID { get; set; }
        public int UserGroupID { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name = "گروه")]
        public string UserGroupName { get; set; }

        [Display(Name = "عکس")]
        public int? ImageId { get; set; }
    }
}
