using Domain.Entities.Base;
using System;

namespace Domain.Entities
{
    public class TodoItem : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Points { get; set; }
    }
}
