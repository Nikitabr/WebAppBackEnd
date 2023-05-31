using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPcRepository : IEntityRepository<App.DAL.DTO.Pc>, IPcRepositoryCustom<Pc>
{
    // write your custom methods here 

}

public interface IPcRepositoryCustom<TEntity>
{
    // write your custom methods here 
}