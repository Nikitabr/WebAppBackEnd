using System.Linq;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Order = App.DAL.DTO.Order;


namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseEntityRepository<App.DAL.DTO.Order, App.Domain.Order, AppDbContext>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Order, App.Domain.Order> mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<App.DAL.DTO.Order>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Include(u => u.Invoices)
            .Include(u => u.ProductInOrders);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }


    public async Task<IEnumerable<Order>> GetAllOrdersByUserId(string userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Include(u => u.Invoices)
            .Include(u => u.ProductInOrders)
            .Where(u => u.AppUserId.ToString() == userId);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    

}