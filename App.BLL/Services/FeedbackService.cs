using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class FeedbackService : BaseEntityService<App.BLL.DTO.Feedback, App.DAL.DTO.Feedback, IFeedbackRepository>,
    IFeedbackService
{
    public FeedbackService(IFeedbackRepository repository, IMapper<Feedback, DAL.DTO.Feedback> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Feedback>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;    }
}