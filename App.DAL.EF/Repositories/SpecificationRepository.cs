using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SpecificationRepository : BaseEntityRepository<App.DAL.DTO.Specification, App.Domain.Specification, AppDbContext>, ISpecificationRepository
{
    public SpecificationRepository(AppDbContext dbContext, IMapper<Specification, Domain.Specification> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Specification>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
        
    }
}