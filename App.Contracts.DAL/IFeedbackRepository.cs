using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IFeedbackRepository : IEntityRepository<App.DAL.DTO.Feedback>, IFeedbackRepositoryCustom<Feedback>
{
    
}

public interface IFeedbackRepositoryCustom<TEntity>
{
    // write your custom methods here 

}