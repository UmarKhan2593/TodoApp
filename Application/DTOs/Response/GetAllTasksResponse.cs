using Domain.Entities;

namespace Application.DTOs.Response
{
    public class GetAllTasksResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Points { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssigenedTo { get; set; }
    }
}
