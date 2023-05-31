using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class PcService : BaseEntityService<App.BLL.DTO.Pc, App.DAL.DTO.Pc, IPcRepository>, IPcService
{
    public PcService(IPcRepository repository, IMapper<BLL.DTO.Pc, DAL.DTO.Pc> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Pc>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync()).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
     }

    public new async Task<Pc?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}