using Domain.Entities.Base;

namespace Domain.Entities
{
    public class TodoItem : BaseEntity<int>
    {
        public string Name { get; set; }
        public int? Points { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssigenedTo { get; set; }
    }

    public enum TaskStatus
    {
        Pending = 1,
        InProgress = 2,
        Complete = 3
    }
}
