using Base.Contract.Domain;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace Base.BLL;

public class BaseEntityService<TBLLEntity, TDalEntity, TRepository> : BaseEntityService<TBLLEntity, TDalEntity, TRepository, Guid>, IEntityService<TBLLEntity>
where TDalEntity : class, IDomainEntityId 
where TBLLEntity : class, IDomainEntityId
where TRepository : IEntityRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TBLLEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}


public class BaseEntityService<TBLLEntity, TDalEntity, TRepository, TKey> : IEntityService<TBLLEntity, TKey>
    where TBLLEntity : class, IDomainEntityId<TKey>
    where TRepository: IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{

    protected TRepository Repository;
    protected IMapper<TBLLEntity, TDalEntity> Mapper;
    public BaseEntityService(TRepository repository, IMapper<TBLLEntity, TDalEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    
    public TBLLEntity Add(TBLLEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBLLEntity Update(TBLLEntity entity)
    {

        return Mapper.Map(Repository.Update(Mapper.Map(entity)!))!;
    }

    public TBLLEntity Remove(TBLLEntity entity)
    {
        return Mapper.Map(Repository.Remove(Mapper.Map(entity)!))!;
    }

    public TBLLEntity Remove(TKey id)
    {
        return Mapper.Map(Repository.Remove(id))!;
    }

    public TBLLEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(Repository.FirstOrDefault(id, noTracking))!;
    }

    public IEnumerable<TBLLEntity> GetAll(bool noTracking = true)
    {
        return Repository.GetAll(noTracking).Select(x => Mapper.Map(x)!);
    }

    public bool Exists(TKey id)
    {
        return Repository.Exists(id);
    }

    public async Task<TBLLEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
    }

    public async Task<IEnumerable<TBLLEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!);
    }

    public Task<bool> ExistsAsync(TKey id)
    {
        return Repository.ExistsAsync(id);
    }

    public async Task<TBLLEntity> RemoveAsync(TKey id)
    {
        return Mapper.Map(await Repository.RemoveAsync(id))!;
    }
}