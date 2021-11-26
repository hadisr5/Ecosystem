using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.ViewModel.Core
{
    public class UserAccessReadViewModel
    {
        public int UserID { get; set; }
        public List<UserAccessPermissionViewModel> Accesses { get; set; }
    }

    public class UserAccessPermissionViewModel
    {
        public int AccessID { get; set; }
        public string AccessTitle { get; set; }
        public bool HasAccess { get; set; }
    }
}
