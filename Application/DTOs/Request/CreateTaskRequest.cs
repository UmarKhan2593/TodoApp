using System;

namespace Application.DTOs.Request
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime? DatePublished { get; set; }
        public int? ReferenceCount { get; set; }
        public int? NumberofCitations { get; set; }

        public int? NumberOfRead { get; set; }
    }
}
