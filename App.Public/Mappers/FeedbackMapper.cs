using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class FeedbackMapper : BaseMapper<App.Public.DTO.v1.Feedback, App.BLL.DTO.Feedback>
{
    public FeedbackMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Feedback ToPublic(App.BLL.DTO.Feedback feedback)
    {
        return new App.Public.DTO.v1.Feedback()
        {
            AppUserId = feedback.AppUserId,
            Description = feedback.Description,
            Id = feedback.Id,
            PcId = feedback.PcId,
            ProductId = feedback.ProductId,
            Rating = feedback.Rating
        };
    }
    
    public static App.BLL.DTO.Feedback ToBll(App.Public.DTO.v1.Feedback feedback)
    {
        return new App.BLL.DTO.Feedback()
        {
            AppUserId = feedback.AppUserId,
            Description = feedback.Description,
            Id = feedback.Id,
            PcId = feedback.PcId,
            ProductId = feedback.ProductId,
            Rating = feedback.Rating
        };
    }
}