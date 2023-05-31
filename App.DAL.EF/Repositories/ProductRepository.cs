using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Product = App.DAL.DTO.Product;

namespace App.DAL.EF.Repositories;

public class ProductRepository : BaseEntityRepository<App.DAL.DTO.Product, App.Domain.Product, AppDbContext>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Product, App.Domain.Product> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync( bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.SpecificationTypes)!
            .ThenInclude(p => p.Specifications)
            .Include(p => p.Feedbacks)
            .Include(p => p.ProductInPcs)
            .Include(p => p.ProductInOrders);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<Product>> GetProductByName(string productName)
    {
        var query = CreateQuery();
        query = query
            .Include(p => p.Feedbacks)
            .Include(p => p.SpecificationTypes)!
            .ThenInclude(p => p.Specifications)
            .Include(p => p.ProductInOrders)
            .Include(p => p.ProductInPcs)
            .Where(p => p.Title.ToLower().Contains(productName.ToLower()));


        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}