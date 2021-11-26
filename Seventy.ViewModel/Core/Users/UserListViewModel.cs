using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seventy.ViewModel.Core.Users
{
   public class UserListViewModel
    {
        public int?  ID { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegDate { get; set; }

        [Display(Name = "نقش")]
        public List<string> Roles { get; set; }

    }
}
