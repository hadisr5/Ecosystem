using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.ViewModel.Core
{
    public class DefaultRoleAccessReadViewModel
    {
        public int RoleID { get; set; }
        public List<RoleAccessPermissionViewModel> Accesses { get; set; }
    }
    public class RoleAccessPermissionViewModel
    {
        public int AccessID { get; set; }
        public string AccessTitle { get; set; }
        public bool HasAccess { get; set; }
    }
}
