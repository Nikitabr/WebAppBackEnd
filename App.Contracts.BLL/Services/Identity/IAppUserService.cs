using App.BLL.DTO.Identity;
using App.Contracts.DAL.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services.Identity;

public interface IAppUserService : IEntityService<App.BLL.DTO.Identity.AppUser>, IAppUserRepositoryCustom<AppUser>
{
    
}