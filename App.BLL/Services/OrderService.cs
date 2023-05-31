using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class OrderService: BaseEntityService<App.BLL.DTO.Order, App.DAL.DTO.Order, IOrderRepository>, IOrderService
{
    public OrderService(IOrderRepository repository, IMapper<BLL.DTO.Order, DAL.DTO.Order> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Order>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res;

    }

    public async Task<IEnumerable<Order>> GetAllOrdersByUserId(string userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllOrdersByUserId(userId, noTracking))
            .Select(x => Mapper.Map(x)!).ToList();

        return res;
    }

    public Order Add(Order order, List<string> products, IEnumerable<Product> productsList)
    {
        decimal price = 0;
        foreach (var product in products)
        {
            price += productsList.Where(p => p.Id.ToString() == product).ElementAt(0).Price;
        }
        order.Price = price;
        
        return Mapper.Map(Repository.Add(Mapper.Map(order)!))!;
    }

}