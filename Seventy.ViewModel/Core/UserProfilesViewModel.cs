namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;

    public class UserProfilesViewModel : CoreBaseViewModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Father { get; set; }
        public string Sex { get; set; }
        public string Tavalod { get; set; }
        public string CodeMelli { get; set; }
        public string Country { get; set; }
        public string Ostan { get; set; }
        public string Shahr { get; set; }
        public string OstanSokoonat { get; set; }
        public string ShahrSokoonat { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Cell { get; set; }
        public string Madrak { get; set; }
        public string Reshte { get; set; }
        public string Daneshgah { get; set; }
        public int? PhotoFileID { get; set; }

    }
}
