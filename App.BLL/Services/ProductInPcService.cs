using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ProductInPcService : BaseEntityService<App.BLL.DTO.ProductInPc, App.DAL.DTO.ProductInPc, IProductInPcRepository>,
    IProductInPcService
{
    public ProductInPcService(IProductInPcRepository repository, IMapper<ProductInPc, DAL.DTO.ProductInPc> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<ProductInPc>> GetAllAsync( bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }
}