using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Hypermedia.Utils
{
    public class PageSearchVO<T> where T : ISupportsHyperMedia
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResult { get; set; }
        public string SortFields { get; set; } = string.Empty;
        public string SortDirections { get; set; } = string.Empty;

        public Dictionary<string, Object> Filters { get; set; }

        public List<T> List { get; set;}

        public PageSearchVO()
        {
        }

        public PageSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PageSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }

        public PageSearchVO(int currentPage, string sortFields, string sortDirections) : this(currentPage,10, sortFields, sortDirections)
        {}

        public int GetCurrentPage()
        { 
            return CurrentPage ==0 ? 2 : CurrentPage; 
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
