using System.Collections.Generic;

namespace TaskManager.Presentation.Web.Infrastructure.CommonDTOs
{
    public class DataTableModel<T> where T : class
    {
        public int? draw { get; set; }
        public long? recordsTotal { get; set; }
        public long? recordsFiltered { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}
