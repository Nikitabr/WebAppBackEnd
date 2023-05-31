using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IPcService : IEntityService<App.BLL.DTO.Pc>, IPcRepositoryCustom<App.BLL.DTO.Pc>
{
}

public interface IPcRepositoryCustom<TEntity>
{


}
