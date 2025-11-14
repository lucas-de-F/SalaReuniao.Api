namespace SalaReuniao.Domain.ValueObject
{
    public class PagedResult<T>
    {
        public ICollection<T> Items { get; private set; }
        public int TotalItems { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PagedResult(ICollection<T> items, int totalItems, int page, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }
    }
}
