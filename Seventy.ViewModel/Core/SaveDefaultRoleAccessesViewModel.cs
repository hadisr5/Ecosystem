using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.ViewModel.Core
{
    public class SaveDefaultRoleAccessesViewModel
    {
        public int RoleID { get; set; }
        public List<int> Accesses { get; set; }
    }
}
