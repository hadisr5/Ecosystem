using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.ViewModel.Core
{
    public class SaveAccessPermissionGroupViewModel
    {
        public int PermissionGroupID { get; set; }
        public List<int> Accesses { get; set; }
    }
}
