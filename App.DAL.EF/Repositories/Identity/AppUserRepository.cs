using App.Contracts.DAL.Identity;
using App.DAL.DTO.Identity;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories.Identity;

public class AppUserRepository : BaseEntityRepository<AppUser, Domain.Identity.AppUser, AppDbContext>, IAppUserRepository
{
    public AppUserRepository(AppDbContext dbContext, IMapper<AppUser, Domain.Identity.AppUser> mapper) : base(dbContext,
        mapper)
    {
        
    }


    public override async Task<AppUser?> FirstOrDefaultAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        query = query.Include(u => u.Feedbacks)
            .Include(u => u.Orders)
            .Include(u => u.Services);

        return Mapper.Map(await query.FirstOrDefaultAsync(u => u.Id == userId));
    }
}