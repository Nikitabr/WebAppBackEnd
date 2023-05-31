using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL.Identity;
using Base.BLL;
using Base.Contracts.BLL;

namespace App.BLL.Services.Identity;

public class RefreshTokenService  : BaseEntityService<App.BLL.DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken, IRefreshTokenRepository>,
    IRefreshTokenService
{
    public RefreshTokenService(IRefreshTokenRepository repository, IMapper<RefreshToken, DAL.DTO.Identity.RefreshToken> mapper) : base(repository,
        mapper)
    {
    }


    public async Task<IEnumerable<RefreshToken>> GetRefreshTokensByUser(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetRefreshTokensByUser(userId, noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }
}