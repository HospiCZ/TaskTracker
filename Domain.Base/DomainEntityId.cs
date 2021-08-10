using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public class DomainEntityId : IDomainEntityId
    {
        [Key]
        public Guid Id { get; set; }
    }
}