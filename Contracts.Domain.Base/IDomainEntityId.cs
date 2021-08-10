using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityId
    {
        public Guid Id { get; set; }
    }
}