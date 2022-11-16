using Domain.Entities.Base;

namespace Domain.Entities
{
    public class UserTodoItem : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
