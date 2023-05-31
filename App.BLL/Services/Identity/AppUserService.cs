using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL.Identity;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;

namespace App.BLL.Services.Identity;

public class AppUserService : BaseEntityService<AppUser, App.DAL.DTO.Identity.AppUser, IAppUserRepository>, IAppUserService
{
    public AppUserService(IAppUserRepository repository, IMapper<AppUser, DAL.DTO.Identity.AppUser> mapper) : base(
        repository, mapper)
    {
        
    }

    public new async Task<AppUser?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }

}