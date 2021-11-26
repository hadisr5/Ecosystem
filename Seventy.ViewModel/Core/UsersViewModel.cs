
namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;

    public class UsersViewModel : CoreBaseViewModel
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}
