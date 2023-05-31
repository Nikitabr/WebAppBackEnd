using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ServiceService : BaseEntityService<App.BLL.DTO.Service, App.DAL.DTO.Service, IServiceRepository>,
    IServiceService
{
    public ServiceService(IServiceRepository repository, IMapper<Service, DAL.DTO.Service> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Service>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }
}