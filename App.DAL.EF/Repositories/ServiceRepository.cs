using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ServiceRepository : BaseEntityRepository<App.DAL.DTO.Service, App.Domain.Service, AppDbContext>, IServiceRepository
{
    public ServiceRepository(AppDbContext dbContext, IMapper<Service, Domain.Service> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Service>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}