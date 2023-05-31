using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using ProductType = App.DAL.DTO.ProductType;

namespace App.DAL.EF.Repositories;

public class ProductTypeRepository : BaseEntityRepository<App.DAL.DTO.ProductType, App.Domain.ProductType, AppDbContext>, IProductTypeRepository
{
    public ProductTypeRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.ProductType, App.Domain.ProductType> mapper) : base(dbContext, mapper)
    {
        
    }
    
    public override async Task<ProductType?> FirstOrDefaultAsync(Guid id,bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return Mapper.Map(await query.FirstOrDefaultAsync(t => t.Id == id));
    }


    public override async Task<IEnumerable<ProductType>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
        
    }
}