using App.Contracts.DAL.Identity;
using App.DAL.DTO.Identity;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories.Identity;

public class RefreshTokenRepository : BaseEntityRepository<App.DAL.DTO.Identity.RefreshToken, App.Domain.Identity.RefreshToken, AppDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext dbContext, IMapper<RefreshToken, Domain.Identity.RefreshToken> mapper) : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<RefreshToken>> GetRefreshTokensByUser(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);    
    }
}