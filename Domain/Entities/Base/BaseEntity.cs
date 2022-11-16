using Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity<TId> : IEntity<TId> //, ISoftDelete,ISuspendable
    {
        [Key]
        public TId Id { get; set; }
    }
}
