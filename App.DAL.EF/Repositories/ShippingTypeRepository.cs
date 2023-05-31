using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShippingTypeRepository : BaseEntityRepository<App.DAL.DTO.ShippingType, App.Domain.ShippingType, AppDbContext>, IShippingTypeRepository
{
    public ShippingTypeRepository(AppDbContext dbContext, IMapper<ShippingType, Domain.ShippingType> mapper) : base(dbContext, mapper)
    {
    }


    public override async Task<IEnumerable<ShippingType>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}