namespace Application.DTOs.Generic
{
    public class PagedRequestParameters : SearchableParameters
    {
        public PagedRequestParameters()
        {
            PageNumber = 1;
            SearchString = "";
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }


        public string OrderBy { get; set; }


    }
}
