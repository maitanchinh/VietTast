namespace Vietast.Services.ProductAPI.Models
{
    public class Filter<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public string? SortBy { get; set; }
        public string SortOrder { get; set; } = "asc";
        public T Criteria { get; set; }
    }
}
