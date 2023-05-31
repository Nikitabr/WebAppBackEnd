using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class SpecificationService : BaseEntityService<App.BLL.DTO.Specification, App.DAL.DTO.Specification, ISpecificationRepository>,
    ISpecificationService
{
    public SpecificationService(ISpecificationRepository repository, IMapper<Specification, DAL.DTO.Specification> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Specification>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;
        
    }

}