using DefaultIdType = System.Guid;

namespace Domain.Common.Contracts
{
    public class BaseEntity : BaseEntity<DefaultIdType>
    {
        protected BaseEntity() => Id = Guid.NewGuid();
    }

    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default!;

        public List<DomainEvent> DomainEvents { get; } = new();
    }
}
