using Seventy.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Seventy.DomainClass.Core
{
    public class LogsViewModel : CoreBaseViewModel
    {
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "بخش")]
        public string Section { get; set; }

        [Display(Name = "نوع")]
        public string LogType { get; set; }

        public string IP { get; set; }
        public string MAC { get; set; }
    }
}
