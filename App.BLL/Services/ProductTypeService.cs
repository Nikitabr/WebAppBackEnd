using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ProductTypeService: BaseEntityService<App.BLL.DTO.ProductType, App.DAL.DTO.ProductType, IProductTypeRepository>, IProductTypeService
{
    public ProductTypeService(IProductTypeRepository repository, IMapper<BLL.DTO.ProductType, DAL.DTO.ProductType> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<ProductType>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;    
    }

}