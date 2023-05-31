using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShippingInfoRepository : BaseEntityRepository<App.DAL.DTO.ShippingInfo, App.Domain.ShippingInfo, AppDbContext>, IShippingInfoRepository
{
    public ShippingInfoRepository(AppDbContext dbContext, IMapper<ShippingInfo, Domain.ShippingInfo> mapper) : base(dbContext, mapper)
    {
    }


    public override async Task<IEnumerable<ShippingInfo>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}