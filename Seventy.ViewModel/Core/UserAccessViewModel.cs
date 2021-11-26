namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;

    public class UserAccessViewModel : CoreBaseViewModel
    {
        public int AccessUserID { get; set; }
        public string AccessType { get; set; }
        public int AccessID { get; set; }
    }
}
