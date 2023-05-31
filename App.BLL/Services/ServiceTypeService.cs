using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ServiceTypeService : BaseEntityService<App.BLL.DTO.ServiceType, App.DAL.DTO.ServiceType, IServiceTypeRepository>,
    IServiceTypeService
{
    public ServiceTypeService(IServiceTypeRepository repository, IMapper<ServiceType, DAL.DTO.ServiceType> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<ServiceType>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }
}