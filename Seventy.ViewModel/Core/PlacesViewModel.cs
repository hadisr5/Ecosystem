using Seventy.ViewModel;

namespace Seventy.DomainClass.Core
{
    public class PlacesViewModel : CoreBaseViewModel
    {
        public int? LayerID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
