using System.Linq;

namespace TaskManager.Presentation.Web.Infrastructure.Routes.Identity.Account
{
    public static class AccountEndpoints
    {
        public static string ADD(string origin) => $"api/Account/Create?origin={origin}";

        public static readonly string Edit = $"api/Account/Update";
        public static string Delete(string Id) => $"api/Account/{Id}";

        public static string GetById(string Id) => $"api/Account/{Id}";

        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/Account?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                url += string.Join(',', orderBy);
            }
            return url;
        }
        public static string GetAllConsumerAsync(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/Account/GetAllConsumerAsync?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                url += string.Join(',', orderBy);
            }
            return url;
        }


    }
}
