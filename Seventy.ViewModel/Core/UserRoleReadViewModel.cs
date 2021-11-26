using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.ViewModel.Core
{
    public class UserRoleReadViewModel
    {
        public int UserID { get; set; }
        public List<UserRolePermissionViewModel> Roles { get; set; }
    }

    public class UserRolePermissionViewModel
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public bool HasRole { get; set; }
    }
}
