using App.Contracts.DAL;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class FeedbackRepository : BaseEntityRepository<App.DAL.DTO.Feedback, App.Domain.Feedback, AppDbContext>, IFeedbackRepository
{
    public FeedbackRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Feedback, App.Domain.Feedback> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<App.DAL.DTO.Feedback>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}