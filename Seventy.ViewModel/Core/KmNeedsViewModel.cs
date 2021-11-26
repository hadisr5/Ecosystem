
namespace Seventy.DomainClass.Core
{
    using Seventy.ViewModel;
    using System;

    public class KmNeedsViewModel:CoreBaseViewModel
    {
        public int UserID { get; set; }
        public string Section { get; set; }
        public int CatID { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
