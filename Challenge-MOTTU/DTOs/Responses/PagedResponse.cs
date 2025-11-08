namespace Challenge_MOTTU.DTOs.Responses
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; } 
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IDictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }
}
