using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ServiceTypeRepository : BaseEntityRepository<App.DAL.DTO.ServiceType, App.Domain.ServiceType, AppDbContext>, IServiceTypeRepository
{
    public ServiceTypeRepository(AppDbContext dbContext, IMapper<ServiceType, Domain.ServiceType> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<ServiceType>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}