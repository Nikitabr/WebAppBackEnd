using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class SpecificationTypeRepository : BaseEntityRepository<App.DAL.DTO.SpecificationType, App.Domain.SpecificationType, AppDbContext>, ISpecificationTypeRepository
{
    public SpecificationTypeRepository(AppDbContext dbContext, IMapper<SpecificationType, Domain.SpecificationType> mapper) : base(dbContext, mapper)
    {
    }


    public override async Task<IEnumerable<SpecificationType>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(s => s.Specifications);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
        
    }
}