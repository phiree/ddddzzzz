using System;

namespace Common.Domain
{
    public interface IDomainEntity
    {
        Guid Id { get; }
    }
}
