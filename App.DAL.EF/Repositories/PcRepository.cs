using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Pc = App.DAL.DTO.Pc;

namespace App.DAL.EF.Repositories;

public class PcRepository : BaseEntityRepository<App.DAL.DTO.Pc, App.Domain.Pc, AppDbContext>, IPcRepository
{
    public PcRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Pc, App.Domain.Pc> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Pc>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(pc => pc.Orders)
            .Include(pc => pc.Feedbacks)
            .Include(pc => pc.ProductInPcs);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);    
    }

    public override async Task<Pc?> FirstOrDefaultAsync(Guid id,bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        
        query = query
            .Include(pc => pc.Orders)
            .Include(pc => pc.Feedbacks)
            .Include(pc => pc.ProductInPcs);

        return Mapper.Map(await query.FirstOrDefaultAsync(pc => pc.Id == id));
    }
    
}