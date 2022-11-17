using Domain.Entities;

namespace Application.DTOs.Request
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }
        public int? Points { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssigenedTo { get; set; }
    }
}
