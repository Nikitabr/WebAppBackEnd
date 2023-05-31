using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IFeedbackService : IEntityService<App.BLL.DTO.Feedback>, IFeedbackRepositoryCustom<App.BLL.DTO.Feedback>
{

}

public interface IFeedbackRepositoryCustom<TEntity>
{

}