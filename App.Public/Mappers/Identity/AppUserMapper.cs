using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using AutoMapper;
using Base.DAL;
using RefreshToken = App.BLL.DTO.Identity.RefreshToken;

namespace App.Public.Mappers.Identity;

public class AppUserMapper : BaseMapper<AppUser, BLL.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
        
    }

    public static AppUser ToPublic(BLL.DTO.Identity.AppUser appUser)
    {
        return new AppUser
        {
            Id = appUser.Id,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Feedbacks = appUser.Feedbacks != null
                ? appUser.Feedbacks.Select(FeedbackMapper.ToPublic).ToList()
                : new List<Feedback>(),
            Orders = appUser.Orders != null ? appUser.Orders.Select(OrderMapper.ToPublic).ToList() : new List<Order>(),
            Services = appUser.Services != null
                ? appUser.Services.Select(ServiceMapper.ToPublic).ToList()
                : new List<Service>(),
        };
    }

    public static BLL.DTO.Identity.AppUser ToBll(AppUser appUser)
    {
        return new BLL.DTO.Identity.AppUser()
        {
            Id = appUser.Id,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Feedbacks = appUser.Feedbacks != null
                ? appUser.Feedbacks.Select(FeedbackMapper.ToBll).ToList()
                : new List<BLL.DTO.Feedback>(),
            Orders = appUser.Orders != null ? appUser.Orders.Select(OrderMapper.ToBll).ToList() : new List<BLL.DTO.Order>(),
            Services = appUser.Services != null
                ? appUser.Services.Select(ServiceMapper.ToBll).ToList()
                : new List<BLL.DTO.Service>(),
        };
    }
}