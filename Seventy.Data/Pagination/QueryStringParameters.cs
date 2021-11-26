namespace Seventy.Data
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public int Start
        {
            set
            {
                if (value > 0)
                {
                    PageNumber = value / PageSize;
                    PageNumber++;
                }
            }
        }
    }
}
