using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class FeedbackMapper : BaseMapper<App.BLL.DTO.Feedback, App.DAL.DTO.Feedback>
{
    public FeedbackMapper(IMapper mapper) : base(mapper)
    {
    }
}