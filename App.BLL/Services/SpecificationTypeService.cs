using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class SpecificationTypeService : BaseEntityService<App.BLL.DTO.SpecificationType, App.DAL.DTO.SpecificationType, ISpecificationTypeRepository>,
    ISpecificationTypeService
{
    public SpecificationTypeService(ISpecificationTypeRepository repository, IMapper<SpecificationType, DAL.DTO.SpecificationType> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<SpecificationType>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;    }
    
}