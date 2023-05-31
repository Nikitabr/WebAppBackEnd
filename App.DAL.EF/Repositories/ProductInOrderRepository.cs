using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductInOrderRepository : BaseEntityRepository<ProductInOrder, Domain.ProductInOrder, AppDbContext>, IProductInOrderRepository
{
    public ProductInOrderRepository(AppDbContext dbContext, IMapper<ProductInOrder, Domain.ProductInOrder> mapper) :
        base(dbContext, mapper)
    {
        
    }
    
    public override async Task<IEnumerable<ProductInOrder>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
        
    }
}