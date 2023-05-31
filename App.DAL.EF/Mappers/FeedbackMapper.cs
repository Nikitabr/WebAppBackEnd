using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class FeedbackMapper : BaseMapper<App.DAL.DTO.Feedback, App.Domain.Feedback>
{
    public FeedbackMapper(IMapper mapper) : base(mapper)
    {
    }
}