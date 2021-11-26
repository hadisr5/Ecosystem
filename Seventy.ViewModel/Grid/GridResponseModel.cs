namespace Seventy.ViewModel
{ 
    public class GridResponseModel
    {
        public int draw { get; set; }
        public long recordsFiltered { get; set; }
        public long recordsTotal { get; set; }
        public object data { get; set; }
        public object AdditionalData { get; set; }
    }

}