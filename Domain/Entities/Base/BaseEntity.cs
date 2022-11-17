using Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity<TId> : IEntity<TId> , ISoftDelete
    {
        [Key]
        public TId Id { get; set; }
        public DateTime CretaedOn { get; set; }
        public int Createdby { get; set; }
        public bool? IsDeleted { get ; set ; }
    }
}
