using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductInPcRepository : BaseEntityRepository<App.DAL.DTO.ProductInPc, App.Domain.ProductInPc, AppDbContext>, IProductInPcRepository
{
    public ProductInPcRepository(AppDbContext dbContext, IMapper<ProductInPc, Domain.ProductInPc> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<ProductInPc>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}