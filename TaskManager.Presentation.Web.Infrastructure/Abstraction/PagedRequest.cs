namespace TaskManager.Presentation.Web.Infrastructure.Abstraction
{
    public class PagedRequest : SearchableRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string[] OrderBy { get; set; }

    }
}
