using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PaymentTypeRepository : BaseEntityRepository<App.DAL.DTO.PaymentType, App.Domain.PaymentType, AppDbContext>, IPaymentTypeRepository
{
    public PaymentTypeRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.PaymentType, App.Domain.PaymentType> mapper) : base(dbContext, mapper)
    {
    }


    public override async Task<IEnumerable<App.DAL.DTO.PaymentType>> GetAllAsync(bool noTracking = true)
    {
        
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
        
    }
}