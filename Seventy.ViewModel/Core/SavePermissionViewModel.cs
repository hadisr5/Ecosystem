using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.ViewModel.Core
{
    public class SavePermissionViewModel
    {
        public int UserID { get; set; }
        public string AccessType { get; set; }
        public List<int> Permissions { get; set; }
    }
}
