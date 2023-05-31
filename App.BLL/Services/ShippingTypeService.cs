using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ShippingTypeService : BaseEntityService<App.BLL.DTO.ShippingType, App.DAL.DTO.ShippingType, IShippingTypeRepository>,
    IShippingTypeService
{
    public ShippingTypeService(IShippingTypeRepository repository, IMapper<ShippingType, DAL.DTO.ShippingType> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<ShippingType>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }

}