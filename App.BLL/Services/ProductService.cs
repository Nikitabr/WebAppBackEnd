using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ProductService: BaseEntityService<App.BLL.DTO.Product, App.DAL.DTO.Product, IProductRepository>, IProductService
{
    public ProductService(IProductRepository repository, IMapper<BLL.DTO.Product, DAL.DTO.Product> mapper) : base(repository, mapper)
    {
    }


    public async Task<IEnumerable<Product>> GetProductByName(string productName)
    {
        var res = (await Repository.GetProductByName(productName))
            .Select(x => Mapper.Map(x)!).ToList();

        return res;
    }
}