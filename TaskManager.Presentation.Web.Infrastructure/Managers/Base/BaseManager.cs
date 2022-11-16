using TaskManager.Presentation.Web.Infrastructure.Interfaces.Managers;
using System;
using System.Net.Http;

namespace TaskManager.Presentation.Web.Infrastructure.Managers.Base
{
    public class BaseManager<T> where T : IManager
    {
        protected readonly HttpClient httpClient;

        public BaseManager(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

    }
}
