using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ShippingInfoService : BaseEntityService<App.BLL.DTO.ShippingInfo, App.DAL.DTO.ShippingInfo, IShippingInfoRepository>,
    IShippingInfoService
{
    public ShippingInfoService(IShippingInfoRepository repository, IMapper<ShippingInfo, DAL.DTO.ShippingInfo> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<ShippingInfo>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }
}