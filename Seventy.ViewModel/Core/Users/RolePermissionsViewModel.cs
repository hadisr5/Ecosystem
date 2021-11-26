using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core.Users
{
  public class RolePermissionsViewModel
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [Display(Name = " نقش")]
        public string RoleName { get; set; }
        [Display(Name = "عنوان ماژول")]
        public string PermissionTitle { get; set; }
        [Display(Name = "ماژول")]
        public string Section { get; set; }
    }
}
