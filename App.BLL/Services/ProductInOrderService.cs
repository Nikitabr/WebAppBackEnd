using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ProductInOrderService : BaseEntityService<App.BLL.DTO.ProductInOrder, App.DAL.DTO.ProductInOrder, IProductInOrderRepository>, IProductInOrderService
{
    public ProductInOrderService(IProductInOrderRepository repository, IMapper<BLL.DTO.ProductInOrder, DAL.DTO.ProductInOrder> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<ProductInOrder>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking))
            .Select(x => Mapper.Map(x)!).ToList();
        
        return res;
    }
}