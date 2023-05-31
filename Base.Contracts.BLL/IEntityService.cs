
using Base.Contract.Domain;
using Base.Contracts.DAL;

namespace Base.Contracts.BLL;

public interface IEntityService<TEntity> : IEntityRepository<TEntity>, IEntityService<TEntity, Guid>
    where TEntity: class, IDomainEntityId
{
    
}

public interface IEntityService<TEntity, TKey> : IEntityRepository<TEntity, TKey>
    where TEntity: class, IDomainEntityId<TKey>
    where TKey: IEquatable<TKey>
{
    
}