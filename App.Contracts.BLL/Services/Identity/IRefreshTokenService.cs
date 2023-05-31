using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services.Identity;

public interface IRefreshTokenService : IEntityService<App.BLL.DTO.Identity.RefreshToken>, IRefreshTokenRepositoryCustom<App.BLL.DTO.Identity.RefreshToken>
{
    
}

public interface IRefreshTokenRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetRefreshTokensByUser(Guid userId, bool noTracking = true);

}