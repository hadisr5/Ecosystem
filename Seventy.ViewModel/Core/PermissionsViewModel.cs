namespace Seventy.DomainClass.Core
{
    using System.ComponentModel.DataAnnotations;
    using Seventy.ViewModel;

    public class PermissionsViewModel:CoreBaseViewModel
    {
        [Display(Name ="عنوان مجوز")]
        public string Title { get; set; }
        [Display(Name ="نام انگلیسی مجوز")]
        public string ENTitle { get; set; }
        [Display(Name ="عنوان ماژول")]
        public string Section { get; set; }
    }
}
